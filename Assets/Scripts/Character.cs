using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : TimeLoopObject
{
    private Animator animator;
    private Rigidbody2D rig;
    private ScreenShake screenShake;


    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundedRadius;
    [SerializeField]
    private float attackDistance;
    [SerializeField]
    private float attackForce;
    private bool isGrounded = false;
    private bool lastGrounded = false;
    private float lastYVelocity = 0f;
    private bool inAttack = false;
    public float speed = 6f;
    public float vSpeedMod = 1f;
    public float jumpForce = 6f;

    private bool facingRight = true;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        screenShake = Camera.main.transform.GetComponent<ScreenShake>();
    }

    void FixedUpdate() {
        // Check if character is grounded
        lastGrounded = isGrounded;
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                isGrounded = true;
        }

        // Check if we just landed
        if (!lastGrounded && isGrounded) {
            screenShake.DoShake(0.1f, lastYVelocity*0.01f, 1f);
        }

        lastYVelocity = rig.velocity.y;

        // Update the animator
        animator.SetFloat("hSpeedMag", Mathf.Abs(rig.velocity.x));
        animator.SetFloat("vSpeed", rig.velocity.y);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("inAttack", inAttack);
    }

    public void Move(float horizontalInput, float verticalInput, bool jump) {
        rig.velocity = new Vector2(horizontalInput * speed, rig.velocity.y + (verticalInput * vSpeedMod));

        if (horizontalInput > 0 && !facingRight) {
            Flip();
        } else if (horizontalInput < 0 && facingRight) {
            Flip();
        }

        if (jump && isGrounded) {
            rig.AddForce(new Vector2(0f, jumpForce));
        }
    }

    void Flip() {
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Attack() {
        if (!inAttack) {
            animator.SetTrigger("attack");
            inAttack = true;
        }

        // Check if we hit anything
        Collider2D[] colliders;
        if (facingRight) {
            colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(attackDistance, 0f, 0f), 1f);
        } else {
            colliders = Physics2D.OverlapCircleAll(transform.position - new Vector3(attackDistance, 0f, 0f), 1f);
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            Attackable attackable = colliders[i].GetComponent<Attackable>();
            if (attackable != null) {
                attackable.Hit(transform, attackForce);
                screenShake.DoShake(0.1f, 0.5f, 1f);
            }
        }
    }

    public void EndAttack() {
        inAttack = false;
    }

    public float GetLastYVelocity() {
        return lastYVelocity;
    }
}

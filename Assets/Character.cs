using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rig;
    [SerializeField]
    private Transform groundCheck;
    private float groundedRadius = 0.1f;
    private bool isGrounded;
    public float speed = 6f;
    public float vSpeedMod = 1f;
    public float jumpForce = 6f;

    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        // Check if character is grounded
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                isGrounded = true;
        }

        // Update the animator
        animator.SetFloat("hSpeedMag", Mathf.Abs(rig.velocity.x));
        animator.SetFloat("vSpeed", rig.velocity.y);
        animator.SetBool("isGrounded", isGrounded);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLoopActivator : TimeLoopObject
{
    private CircleCollider2D circleCollider;
    private Rigidbody2D rig;
    private EndGoal targetEndGoal;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start() {
        rig = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        EndGoal endGoal = collision.transform.GetComponent<EndGoal>();
        if (endGoal != null) {
            // Collided with end goal, activate it
            endGoal.Activate();
            rig.simulated = false;
            targetEndGoal = endGoal;
        }
    }

    void Update() {
        if (targetEndGoal != null) {
            transform.position = Vector3.Lerp(transform.position, targetEndGoal.transform.position, 10f*Time.deltaTime);
        }
    }

    public override void Reset()
    {
        base.Reset();
        targetEndGoal = null;
        rig.simulated = true;    
    }
}

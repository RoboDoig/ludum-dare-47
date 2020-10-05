using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : TimeLoopObject
{

    public float endForce; // How much downward force player needs to exert to activate
    private bool isActive = false;

    protected override void Awake()
    {
        base.Awake();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Character playerCheck = collision.transform.GetComponent<Character>();
        if (playerCheck != null) {
            // Player collided, check enough downward force
            Debug.Log(playerCheck.GetLastYVelocity());
            if (playerCheck.GetLastYVelocity() <= endForce) {
                // level end 
                if (isActive)
                    gameManager.LevelEnd();
            }
        }
    }

    public void Activate() {
        isActive = true;
    }

    public void Deactivate() {
        isActive = false;
    }
}

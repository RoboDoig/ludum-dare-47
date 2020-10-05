using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{

    private GameManager gameManager;
    public float endForce; // How much downward force player needs to exert to activate

    void Start() {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Character playerCheck = collision.transform.GetComponent<Character>();
        Debug.Log(playerCheck);
        if (playerCheck != null) {
            // Player collided, check enough downward force
            if (playerCheck.GetLastYVelocity() <= endForce) {
                // level end 
                gameManager.LevelEnd();
            }
        }
    }
}

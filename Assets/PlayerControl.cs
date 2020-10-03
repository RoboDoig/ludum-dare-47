using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Character character;

    private bool jump = false;

    void Start() {
        character = GetComponent<Character>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            jump = true;
        }
    }

    void FixedUpdate() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (v > 0) {
            v = 0;
        }

        character.Move(h, v, jump);
        jump = false;
    }
}

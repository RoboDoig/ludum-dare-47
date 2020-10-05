using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour
{

    private Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    public void Hit(Transform attacker, float force) {
        Vector2 attackerPosition = new Vector2(attacker.position.x, transform.position.y);
        Vector2 objectPosition = new Vector2(transform.position.x, transform.position.y);

        Vector2 forceVector = ((objectPosition - attackerPosition).normalized)*force;

        rig.AddForce(forceVector);
    }
}

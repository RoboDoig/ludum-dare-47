using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLoopObject : MonoBehaviour
{

    private Vector3 startingPosition;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        startingPosition = transform.position;

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        gameManager.timeLoopObjects.Add(this);
    }

    // Update is called once per frame
    public void Reset() {
        transform.position = startingPosition;
    }
}

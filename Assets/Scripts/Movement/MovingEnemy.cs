using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Speed at which to travel")]
    private float moveForce = 10;
    private bool moveRight = true;
    [SerializeField]
    [Tooltip("Maximum distance to travel")]
    private float maxMovementSpan = 50;
    private float currentMovementSpan = 0;
    [SerializeField]
    [Tooltip("false: move to the right; true: move to the left at startup")]
    private bool reverse = false;

    private void Start()
    {
        if (reverse)
        {
            moveRight = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMovementSpan > maxMovementSpan && currentMovementSpan > 0)
        {
            moveRight = false;
        }
        else if (currentMovementSpan < -maxMovementSpan&& currentMovementSpan < 0)
        {
            moveRight = true;
        }

        if (moveRight == true)
        {
            transform.Translate(new Vector3(moveForce * Time.deltaTime * 70, 0, 0));
            currentMovementSpan += moveForce * Time.deltaTime * 70;
        }

        if (moveRight == false)
        {
            transform.Translate(new Vector3(-moveForce * Time.deltaTime * 70, 0, 0));
            currentMovementSpan -= moveForce * Time.deltaTime * 70;
        }
    }
}

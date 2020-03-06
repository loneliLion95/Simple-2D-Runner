using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    float moveSpeed = -4f;
    float leftPos = -16, rightPos = 1; 

    void Update()
    {
        if (!GameControl.gameStopped)
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            if (transform.position.x < leftPos)
                transform.position = new Vector2(rightPos, transform.position.y);
        }
    }
}

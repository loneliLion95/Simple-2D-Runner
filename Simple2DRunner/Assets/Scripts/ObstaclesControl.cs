using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesControl : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = -4f;
    bool isDead;

    void Update()
    {
        if (!GameControl.gameStopped)
        {
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime,
            transform.position.y);

            if (transform.position.x < -13f)
                Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && !isDead)
        {
            SoundManagerScript.PlaySound("dead");
            isDead = true;
            GameControl.instance.PlayerHit();
        } 
    }
}

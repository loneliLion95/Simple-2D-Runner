using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;
    public GameObject parachute;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isPlanning;
    private bool secondJump;
    public LayerMask layer;

    private CharState State
    {
        set { anim.SetInteger("State", (int)value); }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameControl.gameStopped == false)
        {
            if (IsGrounded())
            {
                Run();
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began && IsGrounded())
                {
                    Jump(14f);
                    secondJump = true;
                }

                if (touch.phase == TouchPhase.Began && secondJump  && !IsGrounded())
                {
                    Jump(11f);
                    secondJump = false;
                }

                if (touch.phase == TouchPhase.Stationary && !secondJump  && rb.velocity.y < 0)
                {
                    isPlanning = true;
                    rb.drag = 9;
                    Fall();
                    parachute.SetActive(true);
                }

                if (isPlanning && touch.phase == TouchPhase.Ended || IsGrounded())
                {
                    isPlanning = false;
                    rb.drag = 1.5f;
                    parachute.SetActive(false);
                }
            }
            else if (Input.touchCount == 0 && !IsGrounded() && rb.velocity.y < 0)
                Fall();
        }
        else
            Dead();
    }

    private void Jump(float jumpForce)
    {
        SoundManagerScript.PlaySound("jump");
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        State = CharState.Jump;
    }

    private void Run()
    {
        State = CharState.Run;
    }

    private void Dead()
    {
        State = CharState.Dead;
        parachute.SetActive(false);
        GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().Stop();
    }

    private void Fall()
    {
        State = CharState.Fall;
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, 0.05f, layer);
        if (raycastHit.collider != null)
            return true;
        else
            return false;
    }

    public void DeadIsDone()
    {
        GameControl.deadIsDone = true;
    }
}

public enum CharState
{
    Run = 1,
    Jump,
    Fall,
    Dead
}



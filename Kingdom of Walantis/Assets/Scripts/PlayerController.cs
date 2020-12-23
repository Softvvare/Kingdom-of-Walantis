using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    public float JumpTakeOffSpeed = 7;
    public float MaxSpeed = 7;
    private bool canDoubleJump;
    private bool LookRight = true;
    private bool falling = false; // check is we are falling

    private Animator anim;


    void Awake()
    {
        anim = GetComponent<Animator>();
        this.gravityModifier = 2f;
    }

    protected override void ComputeVelocity()
    {
        Vector2 MoveP = Vector2.zero;
        MoveP.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                canDoubleJump = true;
                velocity.y = JumpTakeOffSpeed;
            }
            else 
            {
                if (canDoubleJump)
                {
                    velocity.y = 0;
                    canDoubleJump = false;
                    velocity.y += JumpTakeOffSpeed;
                }
            }
        }

        if (!grounded)
            falling = isFalling(velocity);
        else
            falling = isFalling(velocity);

        if (MoveP.x > 0 && !LookRight)
        {
            Flip();
        }
        else if (MoveP.x < 0 && LookRight)
        {
            Flip();
        }

        // set animation parameters
        anim.SetBool("Grounded", grounded);
        anim.SetBool("DoubleJump", canDoubleJump); 
        anim.SetFloat("Speed", Mathf.Abs(MoveP.x));
        anim.SetBool("Falling", falling);

        targetVelocity = MoveP * MaxSpeed;

    }
    private void Flip()
    {
        LookRight = !LookRight;

        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

    private bool isFalling(Vector2 move)
    {
        if (move.y >= 0)
            return false;
        else
            return true;
    }

}

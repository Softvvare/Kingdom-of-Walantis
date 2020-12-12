using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    public float JumpTakeOffSpeed = 7;
    public float MaxSpeed = 7;
    public bool canDoubleJump;
    private bool LookRight = true;


    void Awake()
    {
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
                Debug.Log(velocity.y);
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

        if (MoveP.x > 0 && !LookRight)
        {
            Flip();
        }
        else if (MoveP.x < 0 && LookRight)
        {
            Flip();
        }

        targetVelocity = MoveP * MaxSpeed;

    }
    private void Flip()
    {
        LookRight = !LookRight;

        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

}

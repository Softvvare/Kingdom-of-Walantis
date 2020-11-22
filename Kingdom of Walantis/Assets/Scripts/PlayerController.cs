using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    public float JumpTakeOffSpeed = 7;
    public float MaxSpeed = 7;
    bool canDoubleJump = false;

    void Start()
    {
        
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
                    canDoubleJump = false;
                    velocity.y += JumpTakeOffSpeed;
                    Debug.Log(velocity.y);
                }
            }
            
        }
        
        /*
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.50f;
            }
        }
    */
        targetVelocity = MoveP * MaxSpeed;
    }


}

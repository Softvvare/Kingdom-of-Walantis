using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{

    private Animator anim;

    public int
        maxHealth = 100,
        currentHealth;

    [SerializeField]
    private float
        MaxSpeed;

    [SerializeField]
    private Transform 
        PlayerChecker,
        PlayerObj;

    private bool
        flip,
        canRun,
        LookRight = true;

    [SerializeField]
    private Transform rotateP;

    [SerializeField]
    private LayerMask 
        groundLayer,
        playerLayer;

    public Vector2 MoveP = Vector2.zero;

    // new part 

    [SerializeField]
    private float agroRange;
    
    // end of new part


    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;

        this.gravityModifier = 2f;
        flip = true;
        canRun = true;
        MoveP.x = 1f;
        agroRange = 4;
        MaxSpeed = 3;

    }


    protected override void MoveTowards()
    {
        //transform.position = Vector2.MoveTowards(transform.position, PlayerObj.transform.position, MaxSpeed * Time.deltaTime);

        /*        float distToPlayer = Vector2.Distance(transform.position, PlayerObj.position);

                if (distToPlayer < agroRange)
                {
                    if (transform.position.x < PlayerObj.position.x)
                    {
                        MoveP.x = 1f;
                    }
                    else
                    {
                        MoveP.x = -1f;
                    }
                }*/


        if (CheckFlip())
        {
            if (MoveP.x > 0 && LookRight == true)
            {
                Flip();

                MoveP.x = -1f;
            }
            else if (MoveP.x < 0 && LookRight == false)
            {
                Flip();

                MoveP.x = 1f;
            }
        }
    }
 

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Damage Taken");
        // play hurt animaton ekle, bool isDead
        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died");

        anim.SetBool("isDead", true);
        // die animation  // Enemy Animation Controller

        //disable enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    protected override void ComputeVelocity()
    {

        if (canRun == false) 
        {
            MoveP.x = 0f;
        }


        if (CheckFlip())
        {
            if (MoveP.x > 0 && LookRight == true)
            {
                Flip();

                MoveP.x = -1f;
            }
            else if (MoveP.x < 0 && LookRight == false)
            {
                Flip();

                MoveP.x = 1f;
            }

        }

        // set animation parameters
        // anim.SetBool("Grounded", grounded);

        // anim.SetFloat("Speed", Mathf.Abs(MoveP.x));


        targetVelocity = MoveP * MaxSpeed;
    }

    private void Flip()
    {

        if (CheckFlip() == true)
        {
            Vector3 Scale = transform.localScale;
            Scale.x *= -1;
            transform.localScale = Scale;
            flip = false;
            LookRight = !LookRight;
        }

    }

    public void DisableFlip()
    {
        flip = false;
    }

    public void EnableFlip()
    {
        flip = true;
    }

    public void EnableRun()
    {
        canRun = true;
    }

    public void DisableRun()
    {
        canRun = false;
    }


    public bool DistanceChecker()
    {
        float distToPlayer = Vector2.Distance(transform.position.normalized, PlayerObj.position);

        if (distToPlayer < agroRange)
        {
            Debug.Log("True" + distToPlayer);
            return true;
        }
        else
        {
            Debug.Log("False" + distToPlayer);
            return false;
        }
    }

    public bool CheckFlip()
    {
        Collider2D detectedObject = Physics2D.OverlapCircle(rotateP.position, 0.1f, groundLayer);
        Collider2D detectedPlayer = Physics2D.OverlapCircle(PlayerChecker.position, agroRange, playerLayer);
   

        if (detectedObject == null && detectedPlayer == null)
        {
            if (!DistanceChecker())
            {
                flip = false;
                return false;
            }
            else
            {
                flip = true;
                return true;
            }
            
        }

        else if (detectedObject != null && detectedPlayer == null)
        {
            if (!DistanceChecker())
            {
                flip = true;
                return true;
            }
            else
            {
                flip = true;
                return true;
            }

        }
        
        else if (detectedObject == null && detectedPlayer != null)
        {
            if (DistanceChecker())
            {
                flip = false;
                return false;
            }
            else
            {
                flip = false;
                return false; 
            }

        }
       
        else // if(detectedObject != null && detectedPlayer != null)
        {
            if (DistanceChecker())
            {
                flip = false;
                return false;
            }
            else
            {
                flip = false;
                return false;
            }

        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(PlayerChecker.position, agroRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(rotateP.position, 0.1f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, PlayerObj.position);
        
    }
}


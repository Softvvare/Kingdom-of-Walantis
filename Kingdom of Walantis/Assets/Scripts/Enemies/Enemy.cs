using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{

    private Animator anim;
    
    public int 
        maxHealth = 100,
        currentHealth;

    public float 
        MaxSpeed = 7;

    private Transform target;

    private bool
        flip,
        canRun,
        LookRight = true;

    [SerializeField]
    private Transform rotateP;


    public LayerMask groundLayer;

    public Vector2 MoveP = Vector2.zero;


    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;

        this.gravityModifier = 2f;
        flip = true;
        canRun = true;
        MoveP.x = 1f;
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
                Debug.Log("If" + MoveP);

            }
            else if (MoveP.x < 0 && LookRight == false)
            {
                Flip();

                MoveP.x = 1f;
                Debug.Log("Else" + MoveP);
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
            LookRight = !LookRight;

            Vector3 Scale = transform.localScale;
            Scale.x *= -1;
            transform.localScale = Scale;
            flip = false;
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


    public bool CheckFlip()
    {
        Collider2D detectedObject = Physics2D.OverlapCircle(rotateP.position, 0.1f, groundLayer);

        if (detectedObject == null)
        {
            flip = false;
            return false;
        }
        else
        {
            flip = true;
            return true;
        }
        
        //detectedObject.GetComponent<Enemy>().Flip();
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(rotateP.position, 0.1f);
    }
}

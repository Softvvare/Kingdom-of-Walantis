using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject
{
    [SerializeField]
    private 
        float JumpTakeOffSpeed = 7,
              MaxSpeed = 7;

    private bool canDoubleJump;
    private bool canFlip, canRun;
    private bool LookRight = true;
    private bool falling = false; // check is we are falling
    private bool isDead = false;


    /// <summary>
    [SerializeField]
    private float dashRate = 4.0f;

    [SerializeField]
    private ParticleSystem dash;

    [SerializeField]
    private float actionCooldown = 5.0f;

    private float nextDash;
    private bool isDashing;
    private float timeSinceAction = 5.0f;

    public GameObject loading;
    float waitTime = 3f;

    /// </summary>


    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        this.gravityModifier = 2f;
        canFlip = true;
        canRun = true;
        ///
        isDashing = false;
        /// 
    }


    protected override void ComputeVelocity()
    {

        Vector2 MoveP = Vector2.zero;
        
        MoveP.x = Input.GetAxis("Horizontal");

        if (isDead)
            return;

        if (canRun == false && anim.GetBool("Grounded") == true) //  anim.GetBool("IsAttacking") == true
        {
            MoveP.x = 0f;
        }

        if (!isExaminingOrInventoryOpen())
        {
            MoveP.x = 0f; // issue on jumping
        }


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
        {
            falling = isFalling(velocity);
            anim.SetBool("CanRun", false);
        }    
            
        else
        {
            falling = isFalling(velocity);
            anim.SetBool("CanRun", true);
        }

        if (MoveP.x > 0 && !LookRight)
        {
            Flip();
        }
       
        else if (MoveP.x < 0 && LookRight)
        {
            Flip();
        }


        ///
        timeSinceAction += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.L)&& timeSinceAction > actionCooldown)//&& Time.time > nextDash)
        {
            if (!isDashing )
            {
                timeSinceAction = 0;
                nextDash = Time.time + dashRate;
                MaxSpeed *= 2;
                DashParticle();
            }
        }

        if (Time.time <= nextDash)
        {
            isDashing = true;
        }
        else
        {
            MaxSpeed = 7;
            isDashing = false;
        }
        ///

        // set animation parameters
        anim.SetBool("Grounded", grounded);
        anim.SetBool("DoubleJump", canDoubleJump); 
        anim.SetFloat("Speed", Mathf.Abs(MoveP.x));
        anim.SetBool("Falling", falling);

        targetVelocity = MoveP * MaxSpeed;

    }
    /// <summary>
    private void DashParticle()
    {
        dash.Play();
    }
    /// </summary>
    private void Flip()
    {

        if (canFlip)
        {
            LookRight = !LookRight;

            Vector3 Scale = transform.localScale;
            Scale.x *= -1;
            transform.localScale = Scale;
        }
        
    }

    public void DisableFlip()
    {
        canFlip = false;
    }

    public void EnableFlip()
    {
        canFlip = true;
    }

    public void EnableRun()
    {
        canRun = true;
    }

    public void DisableRun()
    {
        canRun = false;
    } 

    private bool isFalling(Vector2 move)
    {
        if (move.y >= 0)
            return false;
        else
            return true;
    }

    private bool isExaminingOrInventoryOpen()
    {
        bool canMove = true;
        if (FindObjectOfType<InteractionController>().isExamining)
            canMove = false;
        if (FindObjectOfType<InventoryController>().isInventoryOpen)
            canMove = false;
        return canMove;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        
        if (collisionObject.tag == "Lava")
        {
            Hurt(100);
        }
        else if (collisionObject.tag == "Spikes")
        {
            Hurt(100);
        }
    }

    public void Hurt(float damage)
    {
        anim.SetTrigger("GetHurt");
        FindObjectOfType<PlayerAttackController>().FinishAttack();
        FindObjectOfType<HealthController>().LoseHealth(damage);
        this.EnableFlip();
        this.EnableRun();
    }

    public void Die()
    {
        isDead = true;
        anim.SetBool("IsDead", true);
        StartCoroutine(waitforDeath());
    }

    public void Load()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        loading.SetActive(true);
        yield return new WaitForSeconds(3f);
        loading.SetActive(false);
    }

    IEnumerator waitforDeath()
    {
        yield return new WaitForSeconds(1.45f);
        FindObjectOfType<HealthController>().health = 100;
        FindObjectOfType<HealthController>().UpdateBar();
        anim.SetBool("IsDead", false);
        isDead = false;
        FindObjectOfType<LevelController>().Restart();
    }

}

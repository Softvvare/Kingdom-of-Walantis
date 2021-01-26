using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PhysicsObject
{
    [Header("Variables")]
    public float maxSpeed = 2f;
    public bool lookRight = true;
    public bool isAttacking = false;
    public float agroRange = 1.5f;
    public float detectionRange = 2.5f;
    public float attackCooldown = 2.5f;
    public float nextAttackTime = 0f;
    public float damage = 20;
    public float currentHealth = 100f;
    [Header("Player Checks")]
    public Transform playerChecker;
    private Transform playerObject;
    public LayerMask playerLayer;
    [Header("Environment Checks")]
    public Transform rotateP;
    public LayerMask groundLayer;
    [Header("Animator")]
    public Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        this.playerObject = GameObject.FindWithTag("Player").transform;
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;


        if (RotateDetected())
            lookRight = !lookRight;

        if (lookRight)
            move.x = 1f;
        else
            move.x = -1f;

        if (animator.GetBool("IsAttacking"))
            move.x = 0f;

        if (PlayerDetected())
        {
            if (nextAttackTime <= 0)
            {
                nextAttackTime = attackCooldown;
                Attack();
            }
            else
            {
                nextAttackTime -= Time.deltaTime;
                animator.SetBool("IsAttacking", false);
                if (Mathf.Abs(transform.position.x - playerObject.transform.position.x) > agroRange)
                {
                    if ((transform.position.x > playerObject.transform.position.x) && lookRight)
                    {
                        lookRight = !lookRight;
                        Flip();
                    }
                    if ((transform.position.x < playerObject.transform.position.x) && !lookRight)
                    {
                        lookRight = !lookRight;
                        Flip();
                    }
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerObject.transform.position.x, transform.position.y), maxSpeed * Time.deltaTime);
                }
                else
                {
                    move.x = 0f;
                }
            }
        }
        animator.SetFloat("Speed", Mathf.Abs(move.x));
        targetVelocity = move * maxSpeed;
    }

    public bool PlayerDetected()
    {
        Collider2D playerDetected = Physics2D.OverlapCircle(playerChecker.position, detectionRange, playerLayer);

        if (playerDetected)
        {
            //Debug.Log(playerDetected.transform.position); // to get player positions for dynamic use
            return true;
        }
        else
            return false;

    }

    public void Attack()
    {
        float distance = Vector2.Distance(transform.position, playerObject.transform.position);
        if (distance <= agroRange)
        {
            //Debug.Log("Attacking");
            animator.SetBool("IsAttacking", true);
            animator.SetTrigger("Attack");
            playerObject.GetComponent<PlayerController>().Hurt(damage);
            //FindObjectOfType<HealthController>().LoseHealth(5);
        }
        else
        {
           //Debug.Log("not Attacking");
        }
    }


    public bool RotateDetected()
    {
        Collider2D detection = Physics2D.OverlapCircle(rotateP.position, 0.1f, groundLayer);

        if (detection)
        {
            Flip();
            return true;
        }
        else
            return false;
    }

    public void Flip()
    {
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        //disable enemy
        this.GetComponent<Collider2D>().enabled = false;
        
        this.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerChecker.position, agroRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(playerChecker.position, detectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(rotateP.position, 0.1f);
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, playerObject.transform.position);
    }
}

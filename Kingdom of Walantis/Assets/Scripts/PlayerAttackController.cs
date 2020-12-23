using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{

    private Animator anim;
    public Transform attackP;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int attackDamage = 40;
    
    public float attackRate = 1f;// attack cooldown
    private float nextAttackTime = 0f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                nextAttackTime = Time.time + (1f / attackRate);// calculate cooldown
                Attack(1);
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                nextAttackTime = Time.time + (1f / attackRate);// calculate cooldown
                Attack(2);
            }
        }
    }

    private void Attack(int type)
    {
        if (type == 1)
            anim.SetTrigger("IsLightAttacking");
        else if (type == 2)
            anim.SetTrigger("IsAttacking");

        // hitting list
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackP.position, attackRange, enemyLayer);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackP == null)
            return;
        Gizmos.DrawWireSphere(attackP.position, attackRange);
    }
}

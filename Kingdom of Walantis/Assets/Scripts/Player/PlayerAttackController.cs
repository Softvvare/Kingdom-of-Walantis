using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField]
    private bool combatEnabled;

    [SerializeField]
    private float  attackRange = 0.5f,  attackRate = 1f;// attack cooldown

    public int attackDamage = 20, lightAttackDamage = 10;

    [SerializeField]
    private LayerMask whatIsDamageable;
    [SerializeField]
    private Transform attackP;

    protected float nextAttackTime = 0f;

    private bool gotInput, isAttacking;

    private Animator anim;


    public void Start()
    {
        combatEnabled = true;
        isAttacking = false;
        anim = GetComponent<Animator>();
        anim.SetBool("CanAttack", combatEnabled);
        PlayerPrefs.SetInt("lightAttackDamage", lightAttackDamage);
        PlayerPrefs.SetInt("attackDamage", attackDamage);
    }


    public void Update()
    {
        bool grounded = anim.GetBool("Grounded");

        if (!grounded)
        {
            return;
        }
        else
        {
            CheckAttackInput();
        }

    }

    public void CheckAttackInput()
    {
        if (!FindObjectOfType<PlayerController>().isExaminingOrInventoryOpen())
            return;
        if (Input.GetMouseButtonDown(0))
        {
            gotInput = true;
            anim.SetBool("LightAttack", true);
            anim.SetBool("IsAttacking", true);
            anim.SetBool("CanRun", false);
        }
        else if(Input.GetMouseButtonDown(1))
        {
            gotInput = true;
            anim.SetBool("Attack", true);
            anim.SetBool("IsAttacking", true);
            anim.SetBool("CanRun", false);
        }
    }

    public void LightAttack()
    {
        if (gotInput)
        {
            Debug.Log(PlayerPrefs.GetInt("lightAttackDamage"));
            if (!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
            }
        }
    }

    public void Attack()
    {
        if (gotInput)
        {
            Debug.Log(PlayerPrefs.GetInt("attackDamage"));
            if (!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
            }
        }
    }

    public void CheckLightAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackP.position, attackRange, whatIsDamageable);
        foreach(Collider2D collider in detectedObjects)
        {
            // collider.transform.parent.SendMessage("Damage", liteAttackDamage); // call from any other different scripts
            lightAttackDamage = PlayerPrefs.GetInt("lightAttackDamage");
            collider.GetComponent<EnemyController>().TakeDamage(lightAttackDamage);
        }
    }

    public void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackP.position, attackRange, whatIsDamageable);
        foreach (Collider2D collider in detectedObjects)
        {
            // collider.transform.parent.SendMessage("Damage", attackDamage); // call from any other different scripts
            lightAttackDamage = PlayerPrefs.GetInt("attackDamage");
            collider.GetComponent<EnemyController>().TakeDamage(attackDamage);
        }
    }

    public void FinishAttack()
    {
        isAttacking = false;
        gotInput = false;
        anim.SetBool("IsAttacking", false);
        anim.SetBool("LightAttack", false);
        anim.SetBool("Attack", false);
        anim.SetBool("CanRun", true);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackP.position, attackRange);
    }
}

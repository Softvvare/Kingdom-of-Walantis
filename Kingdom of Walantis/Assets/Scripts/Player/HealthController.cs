using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public Image fillBar;
    public float health;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // for testing
            LoseHealth(5);
    }

    public void LoseHealth(float damage)
    {
        if (health <= 0)
            return;

        health -= damage;
        fillBar.fillAmount = health / 100;
        if (health <= 0)
        {
            //Debug.Log("Died");
            FindObjectOfType<PlayerController>().Die();
        }
    }

}

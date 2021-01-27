using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public Image fillBar;
    public float health;
    public float armor;

    private void Update()
    {

    }

    public void LoseHealth(float damage)
    {
        if (health <= 0)
            return;

        health -= (damage - armor);
        UpdateBar();
        if (health <= 0)
        {
            FindObjectOfType<PlayerController>().Die();
        }
    }

    public void UpdateBar()
    {
        fillBar.fillAmount = health / 100;
    }

}

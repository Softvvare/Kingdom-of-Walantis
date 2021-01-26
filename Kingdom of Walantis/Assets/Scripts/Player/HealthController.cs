using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public Image fillBar;
    public float health;
    public float armor;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // for testing
            LoseHealth(5);
    }

    public void LoseHealth(float damage)
    {
        if (health <= 0)
            return;

        health -= (damage - armor);
        //fillBar.fillAmount = health / 100;
        UpdateBar();
        if (health <= 0)
        {
            //Debug.Log("Died");
            FindObjectOfType<PlayerController>().Die();
        }
    }

    public void UpdateBar()
    {
        fillBar.fillAmount = health / 100;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{

    private Animator anim;
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
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

}

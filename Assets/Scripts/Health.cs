using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currHealth;
    

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HealDamage(int heal)
    {
        currHealth += heal;

        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
    }
    public void TakeDamage(int damage)
    {
        currHealth -= damage;
        
        if (currHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;

    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<ItemDrop>()!=null)
            GetComponent<ItemDrop>().isDying = false;
        curHealth = maxHealth;
    }

    public void HealDamage(int heal)
    {
        curHealth += heal;

        if(curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        curHealth -= damage;

        if(curHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (GetComponent<ItemDrop>() != null)
            GetComponent<ItemDrop>().CreateItem();
        Destroy(gameObject);
    }
}

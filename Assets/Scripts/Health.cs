using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public bool isPlayer;
    public float knockbackTime;
    


    private Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        coll = gameObject.GetComponent<Collider2D>();

    }

    public void HealDamage(int heal)
    {
        curHealth += heal;

        if(curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
    }

    public void TakeDamage(int damage,float knockPosition,float knockbackPower)
    {
        curHealth -= damage;
        if (gameObject.tag == "Player")
            isPlayer = true;
        if(curHealth <= 0)
        {
            if(gameObject.GetComponent<ExplosiveController>()==null)
                Die();
            
        }
        Knockback(knockPosition,knockbackPower);
    }

    public void Die()
    {
        if (GetComponent<ItemDrop>() != null)
            GetComponent<ItemDrop>().CreateItem();
        Destroy(gameObject);
    }

    public void Knockback(float knockPosition,float knockbackPower)
    {
        if (isPlayer)
        {
            gameObject.GetComponent<PlayerMovement>().DisableMovement(knockbackTime);
        }
        if (gameObject.transform.position.x >= knockPosition)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce
                (new Vector2(knockbackPower, knockbackPower/2), ForceMode2D.Impulse);
        }
        else if (gameObject.transform.position.x < knockPosition)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce
                (new Vector2(-knockbackPower, knockbackPower/2), ForceMode2D.Impulse);
        }
    }
}

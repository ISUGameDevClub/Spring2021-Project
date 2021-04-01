using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public bool isPlayer;
    public Text healthText;
    public Animator transition;
    public float transitionTime;
    public Animator playerHurtEffect;

    private Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        coll = gameObject.GetComponent<Collider2D>();

        if (isPlayer)
            healthText.text = "Health: " + curHealth;
    }

    public void HealDamage(int heal)
    {
        curHealth += heal;

        if(curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if (isPlayer)
            healthText.text = "Health: " + curHealth;
    }

    public void TakeDamage(int damage,float knockPosition,float knockbackPower, float knockbackTime)
    {
        curHealth -= damage;
        if (gameObject.tag == "Player")
            isPlayer = true;
        if(curHealth <= 0)
        {
            if(gameObject.GetComponent<ExplosiveController>()==null)
                Die();            
        }
        else if(isPlayer)
        {
            playerHurtEffect.SetTrigger("Hurt");
            healthText.text = "Health: " + curHealth;
        }
        Knockback(knockPosition, knockbackPower, knockbackTime);
    }

    public void Die()
    {
        if(GetComponent<Switch>() != null)
        {
            GetComponent<Switch>().HitSwitch();
        }
        if (GetComponent<ItemDrop>() != null)
            GetComponent<ItemDrop>().CreateItem();
        if (isPlayer)
        {
            StartCoroutine(ReloadLevel());
        }
        else
            Destroy(gameObject);

    }

    public void Knockback(float knockPosition, float knockbackPower, float knockbackTime)
    {
        if (isPlayer)
        {
            gameObject.GetComponent<PlayerMovement>().myAnim.SetBool("Hurt", true);
            gameObject.GetComponent<PlayerMovement>().DisableMovement(knockbackTime);
        }

        if (gameObject.transform.position.x >= knockPosition)
        {
            if (gameObject.GetComponent<Rigidbody2D>() != null)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                gameObject.GetComponent<Rigidbody2D>().AddForce
                    (new Vector2(knockbackPower, knockbackPower * .75f), ForceMode2D.Impulse);
            }
        }
        else if (gameObject.transform.position.x < knockPosition)
        {
            if (gameObject.GetComponent<Rigidbody2D>() != null)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                gameObject.GetComponent<Rigidbody2D>().AddForce
                    (new Vector2(-knockbackPower, knockbackPower * .75f), ForceMode2D.Impulse);
            }
        }
    }

    public IEnumerator ReloadLevel()
    {
        transition.SetTrigger("Change Scene");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

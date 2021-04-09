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
    public HealthUI healthText;
    public Animator transition;
    public float transitionTime;
    public Animator playerHurtEffect;
    public GameObject hurtBox;

    private Collider2D coll;
    public GameObject healParticle;
    public GameObject DeathParticle;
    private Coroutine hitBoxCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        coll = gameObject.GetComponent<Collider2D>();

        if (isPlayer)
            healthText.UpdateHealthUI(curHealth);
    }

    public void HealDamage(int heal)
    {
        curHealth += heal;

        if(curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if (isPlayer)
            healthText.UpdateHealthUI(curHealth);
        if (healParticle != null)
        {
            GameObject s = Instantiate(healParticle, transform.position, new Quaternion(0, 0, 0, 0));
            s.transform.SetParent(null);
        }

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

        if(isPlayer)
        {
            playerHurtEffect.SetTrigger("Hurt");
            healthText.UpdateHealthUI(curHealth);
            GetComponent<DashAbility>().ResetDash();
        }
        else if (hurtBox != null)
        {
            if (hitBoxCoroutine != null)
                StopCoroutine(hitBoxCoroutine);
            hitBoxCoroutine = StartCoroutine(DisableHitbox(knockbackTime));
        }
        Knockback(knockPosition, knockbackPower, knockbackTime);
    }

    public IEnumerator DisableHitbox(float knockbackTime)
    {
        hurtBox.SetActive(false);
        yield return new WaitForSeconds(knockbackTime);
        hurtBox.SetActive(true);
    }

    public void Die()
    {
        if (DeathParticle != null)
        {
            GameObject d = Instantiate(DeathParticle, transform.position, new Quaternion(0, 0, 0, 0));
            d.transform.SetParent(null);
        }

        if (GetComponent<Switch>() != null)
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
        else
        {
            if(GetComponent<RangedEnemy>() != null)
            {
                GetComponent<RangedEnemy>().ResetAttack(knockbackTime);
            }
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

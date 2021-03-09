using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
	private Vector2 attackOrigin;
    private bool attackReady;
    private PlayerMovement pm;
	private HurtBox hurt;
	private Collider2D coll;


	public int rangedSpeed;
	public GameObject attackZone;
	public GameObject bullet;
	public int attackPower;
	public float meleeAttackActiveTime;
	public float meleeAttackWindup;
	public float rangedActiveTime;
	public float rangedAttackWindup;

    // Start is called before the first frame update
    void Start()
    {
        attackZone.SetActive(false);
		hurt = attackZone.GetComponent<HurtBox>();
        attackReady = true;
        pm = GetComponent<PlayerMovement>();
		coll = GetComponent<Collider2D>();
	}

    // Update is called once per frame
    void Update()
    {
        if (pm.facingRight)
        {
            if (attackZone != null)
                attackZone.transform.localPosition = (Vector3.right);
        }
        else
        {
            if (attackZone != null)
                attackZone.transform.localPosition = (Vector3.left);
        }

		if (Input.GetKeyDown(KeyCode.Mouse0) && attackReady)
		{
			StartCoroutine(MeleeAttack());
		}		
		else if(Input.GetKeyDown(KeyCode.Mouse1 ) && attackReady)
		{
			StartCoroutine(RangedAttack());
		}

	}

	private IEnumerator MeleeAttack() {
        pm.myAnim.SetTrigger("Melee 1");
        pm.DisableMovement(meleeAttackWindup + meleeAttackActiveTime);
        attackReady = false;
		coll.enabled = false;
		coll.enabled = true;
		yield return new WaitForSeconds(meleeAttackWindup);
		attackZone.SetActive(true);
		hurt.ClearArray();
		yield return new WaitForSeconds(meleeAttackActiveTime);
		attackZone.SetActive(false);
        attackReady = true;
    }

	private IEnumerator RangedAttack() {
        attackReady = false;
        pm.myAnim.SetTrigger("Shoot");
        pm.DisableMovement(rangedAttackWindup + rangedActiveTime);
        yield return new WaitForSeconds(rangedAttackWindup);
		if(pm.facingRight){
		    Bullet bul = Instantiate(bullet,new Vector2(transform.position.x+1,transform.position.y),new Quaternion (0,0,0,0)).gameObject.GetComponent<Bullet>();
		    bul.facingRight = pm.facingRight;
			bul.GetComponent<HurtBox>().isPlayer = true;
            bul.transform.SetParent(null);
		}
		else{
		    Bullet bul = Instantiate(bullet,new Vector2(transform.position.x-1,transform.position.y),new Quaternion (0,0,0,0)).gameObject.GetComponent<Bullet>();
		    bul.facingRight = pm.facingRight;
			bul.GetComponent<HurtBox>().isPlayer = true;
            bul.transform.SetParent(null);
        }
        yield return new WaitForSeconds(rangedActiveTime);
        attackReady = true;
	}	
}

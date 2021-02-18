using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
	private Vector2 attackOrigin;
    private bool meleeReady;
	private bool gunReady;
    private PlayerMovement pm;
	
	public int rangedSpeed;
	public GameObject attackZone;
	public GameObject bullet;
	public int attackPower;
	public float meleeAttackActiveTime;
	public float meleeAttackWindup;
	public float rangedReloadSpeed;
	public float rangedAttackWindup;
    // Start is called before the first frame update
    void Start()
    {
        attackZone.SetActive(false);
		gunReady = true;
        meleeReady = true;
        pm = GetComponent<PlayerMovement>();
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


        if (Input.GetKeyDown(KeyCode.Mouse0) && meleeReady)
        {
			StartCoroutine(MeleeAttack());
		}		
		if(Input.GetKeyDown(KeyCode.Mouse1 ) && gunReady){
			StartCoroutine(RangedAttack());
		}
    }

	private IEnumerator MeleeAttack() {
        meleeReady = false;
        yield return new WaitForSeconds(meleeAttackWindup);
        attackZone.SetActive(true);
		yield return new WaitForSeconds(meleeAttackActiveTime);
        attackZone.SetActive(false);
        meleeReady = true;
    }

	private IEnumerator RangedAttack() {
		gunReady = false;
		yield return new WaitForSeconds(rangedAttackWindup);
		if(pm.facingRight){
		    Bullet bul = Instantiate(bullet,new Vector2(transform.position.x+1,transform.position.y),new Quaternion (0,0,0,0)).gameObject.GetComponent<Bullet>();
		    bul.facingRight = pm.facingRight;
		}
		else{
		    Bullet bul = Instantiate(bullet,new Vector2(transform.position.x-1,transform.position.y),new Quaternion (0,0,0,0)).gameObject.GetComponent<Bullet>();
		    bul.facingRight = pm.facingRight;
		}
		
		yield return new WaitForSeconds(rangedReloadSpeed);
		gunReady = true;
	}	
}

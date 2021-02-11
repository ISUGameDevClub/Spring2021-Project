using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
	private BoxCollider2D collider;
	private Vector2 attackOrigin;
	private bool gunReady;
	private AttackMovement am; 
	
	public int rangedSpeed;
	public GameObject AttackZone;
	public GameObject bullet;
	public int attackPower;
	public float meleeAttackActiveTime;
	public float meleeAttackWindup;
	public float rangedReloadSpeed;
	public float rangedAttackWindup;
    // Start is called before the first frame update
    void Start()
    {
		collider = AttackZone.GetComponent<BoxCollider2D>();
		AttackZone.SetActive(false);
		am = FindObjectOfType<AttackMovement>();
		gunReady = true;
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.Mouse0)){
			StartCoroutine(MeleeAttack());
		}		
		if(Input.GetKeyDown(KeyCode.Mouse1 ) && gunReady){
			StartCoroutine(RangedAttack());
		}
    }
	//private void OnTriggerEnter2D(Collider2D collision){
	//	if(collision.gameObject.tag == "Enemy"){
	//		
	//		collision.gameObject.TakeDamage(attackPower);
	//		
	//	}	
	private IEnumerator MeleeAttack() {
		yield return new WaitForSeconds(meleeAttackWindup);
		AttackZone.SetActive(true);
		yield return new WaitForSeconds(meleeAttackActiveTime);
		AttackZone.SetActive(false);
	}
		private IEnumerator RangedAttack() {
		gunReady = false;
		yield return new WaitForSeconds(rangedAttackWindup);
		if(am.facingRight ){
		Bullet bul = Instantiate(bullet,new Vector2(transform.position.x+1,transform.position.y),new Quaternion (0,0,0,0)).gameObject.GetComponent<Bullet>();
		bul.facingRight=am.facingRight;
		}
		else{
		Bullet bul = Instantiate(bullet,new Vector2(transform.position.x-1,transform.position.y),new Quaternion (0,0,0,0)).gameObject.GetComponent<Bullet>();
		bul.facingRight=am.facingRight;
		}
		
		yield return new WaitForSeconds(rangedReloadSpeed);
		gunReady = true;
	}

	
}

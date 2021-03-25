using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour
{

    public bool LogEvents;

    [Header("DEBUG")]
    public GameObject Target;
    public string[] AttackHistory;
   

    
   private Vector2 attackOrigin;
    private bool attackReady;
 
	private HurtBox HurtBox;
	private Collider2D Coll;
    private AmmoSystem AmmoSys;

    public int rangedSpeed;
	public GameObject attackZone;
	public GameObject bullet;
	public int attackPower;
	public float meleeAttackActiveTime;
	public float meleeAttackWindup;
	public float rangedActiveTime;
	public float rangedAttackWindup;

    public bool isAttacking;
    private Agent _AgentControlHub;
    // Start is called before the first frame update
    public void Start()
    {
        attackZone.SetActive(false);
		HurtBox = attackZone.GetComponent<HurtBox>();
        attackReady = true;
		Coll = GetComponent<Collider2D>();
        AmmoSys = GetComponent<AmmoSystem>();
        _AgentControlHub = GetComponentInParent<Agent>();
	}

    // Update is called once per frame
    public void Update()
    {
        if (_AgentControlHub.AgentState.ForwardDir == Vector3.right)
        {
            if (attackZone != null)
                attackZone.transform.localPosition = (Vector3.right);
        }
        else
        {
            if (attackZone != null)
                attackZone.transform.localPosition = (Vector3.left);
        }

	}

    public void AIAttackExecute(int attackType = -1) 
    {
        switch(attackType)
        {
            case -1:
                break;
            case 1:
                 AttackMelee();
                break;
            case 2:
                AttackRanged();
                break;

            default:
                Debug.Log("Attemted to send inncorect attack type. was: " + attackType.ToString());
                break;
        }
    }

     private void AttackMelee()
    {
        isAttacking = true;
        UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.General, "AI attacked with melee atack" );
        MeleeAttackHelper();
    }

    private void AttackRanged()
    {
        isAttacking = true;
        UnityLoggingDelegate.LogIfTrue(LogEvents, UnityLoggingDelegate.LogType.General, "AI attacked with melee atack" );
        RangedAttackHelper();
    }

    public void MeleeAttackHelper()
    {
        StartCoroutine(MeleeAttack());
    }

    public void RangedAttackHelper()
    {
        AmmoSys.UseAmmo(1);
        StartCoroutine(RangedAttack());
    }

    private void OnAttackFinished()
    {
        isAttacking = false;
    }

    private IEnumerator MeleeAttack() {
        attackReady = false;
		Coll.enabled = false;
		Coll.enabled = true;
		yield return new WaitForSeconds(meleeAttackWindup);
		attackZone.SetActive(true);
		HurtBox.ClearArray();
		yield return new WaitForSeconds(meleeAttackActiveTime);
		attackZone.SetActive(false);
        attackReady = true;
        OnAttackFinished();
    }

	private IEnumerator RangedAttack() {
        attackReady = false;
        yield return new WaitForSeconds(rangedAttackWindup);
		if(_AgentControlHub){
		    Bullet bul = Instantiate(bullet,new Vector2(transform.position.x+1,transform.position.y),new Quaternion (0,0,0,0)).gameObject.GetComponent<Bullet>();
		    bul.facingRight = true;
			bul.GetComponent<HurtBox>().isPlayer = true;
            bul.transform.SetParent(null);
		}
		else{
		    Bullet bul = Instantiate(bullet,new Vector2(transform.position.x-1,transform.position.y),new Quaternion (0,0,0,0)).gameObject.GetComponent<Bullet>();
		    bul.facingRight = false;
			bul.GetComponent<HurtBox>().isPlayer = true;
            bul.transform.SetParent(null);
        }
        yield return new WaitForSeconds(rangedActiveTime);
        attackReady = true;
        OnAttackFinished();
	}	

    
   
}

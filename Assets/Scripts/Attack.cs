﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
	private Vector2 attackOrigin;
    private bool attackReady;
    private PlayerMovement pm;
	private HurtBox hurt;
	private Collider2D coll;
    private AmmoSystem ams;
    private int currentMeleeAttack;
    private int currentPlannedMeleeAttack;
    private Coroutine bufferCoroutine;
    private bool meleeMovement;

    public int rangedSpeed;
	public GameObject attackZone;
	public GameObject bullet;
    public float attackMovementSpeed;
	public float meleeAttackActiveTime;
	public float meleeAttackWindup;
    public float meleeAttackEndTime;
    public float meleeBufferTime;
    public float rangedActiveTime;
	public float rangedAttackWindup;

    

    // Start is called before the first frame update
    public void Start()
    {
        attackZone.SetActive(false);
		hurt = attackZone.GetComponent<HurtBox>();
        attackReady = true;
        pm = GetComponent<PlayerMovement>();
		coll = GetComponent<Collider2D>();
        ams = GetComponent<AmmoSystem>();
	}

    // Update is called once per frame
    public void Update()
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

		if (Input.GetKeyDown(KeyCode.Mouse0) && pm.isGrounded)
		{
            if(bufferCoroutine != null)
                StopCoroutine(bufferCoroutine);
            if (currentMeleeAttack == 0)
            {
                currentPlannedMeleeAttack = 1;
            }
            else if (currentMeleeAttack == 1)
            {
                currentPlannedMeleeAttack = 2;
            }
            else if (currentMeleeAttack == 2)
            {
                currentPlannedMeleeAttack = 3;
            }
        }		
		else if(Input.GetKeyDown(KeyCode.Mouse1) && attackReady && ams.totalAmmo > 0 && pm.canMove && pm.isGrounded)
		{
            ams.UseAmmo(1);
			StartCoroutine(RangedAttack());
		}
        if(meleeMovement)
        {
            if(pm.facingRight)
                transform.Translate(new Vector2(Time.deltaTime * attackMovementSpeed, 0));
            else
                transform.Translate(new Vector2(Time.deltaTime * -attackMovementSpeed, 0));
        }

        if(currentPlannedMeleeAttack != currentMeleeAttack && attackReady && pm.canMove)
        {
            StartCoroutine(MeleeAttack(currentPlannedMeleeAttack));
        }
	}

    public void MeleeAttackHelper()
    {
        StartCoroutine(MeleeAttack(1));
    }

    public void RangedAttackHelper()
    {
        StartCoroutine(RangedAttack());
    }

    private IEnumerator MeleeAttack(int curAttack) {
        currentMeleeAttack = curAttack;
        meleeMovement = true;
        bufferCoroutine = StartCoroutine(MeleeBuffer());
        pm.myAnim.SetTrigger("Melee "+ (curAttack));
        pm.DisableMovement(meleeAttackWindup + meleeAttackActiveTime + meleeAttackEndTime);
        attackReady = false;
		coll.enabled = false;
		coll.enabled = true;
		yield return new WaitForSeconds(meleeAttackWindup);
		attackZone.SetActive(true);
		hurt.ClearArray();
		yield return new WaitForSeconds(meleeAttackActiveTime);
        meleeMovement = false;
        attackZone.SetActive(false);
        yield return new WaitForSeconds(meleeAttackEndTime);
        attackReady = true;
        if(curAttack == 3)
        {
            currentMeleeAttack = 0;
            currentPlannedMeleeAttack = 0;
        }
    }

    private IEnumerator MeleeBuffer()
    { 
        yield return new WaitForSeconds(meleeAttackWindup + meleeAttackActiveTime + meleeAttackEndTime + meleeBufferTime);
        currentMeleeAttack = 0;
        currentPlannedMeleeAttack = 0;
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

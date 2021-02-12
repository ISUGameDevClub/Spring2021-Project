using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMovement : MonoBehaviour
{
	public GameObject attackZone;
	public bool facingRight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetAxis("Horizontal")>0){
			facingRight=true;
			if(attackZone != null)
				attackZone.transform.localPosition = (Vector3.right);
		}
		if(Input.GetAxis("Horizontal")<0){
			facingRight=false;
			if(attackZone != null)
				attackZone.transform.localPosition = (Vector3.left);
		}
    }
}

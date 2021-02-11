using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
	public PlayerMovement pm;
	
	private void OnTriggerEnter2D(Collider2D collision){
		if(collision.gameObject.tag == "Ground"){
			
			pm.isGrounded = true;
			
		}
		

	}
	private void OnTriggerExit2D(Collider2D collision){
		if (collision.gameObject.tag == "Ground"){
	
			pm.isGrounded = false;
			
		}
	
	}
	
}

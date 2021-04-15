using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
	
	public bool facingRight;
	public float speed;
	public float despawnTime;

	private HurtBox hb;
	// Start is called before the first frame update
	void Start()
    {
        Destroy(gameObject,despawnTime);
		hb = GetComponent<HurtBox>();
	}

    // Update is called once per frame
    void Update()
    {
        if(facingRight){
			transform.Translate(new Vector2(speed*Time.deltaTime,0));
		}
		else{
            GetComponent<SpriteRenderer>().flipX = true;
			transform.Translate(new Vector2(-speed*Time.deltaTime,0));
		}
    }

	private void OnTriggerEnter2D(Collider2D collision){
		if(collision.gameObject.tag == "Ground"||(collision.gameObject.tag == "Enemy" && hb.isPlayer )||(collision.gameObject.tag == "Player" && !hb.isPlayer))
		{
            if(GetComponent<ExplosiveController>() != null)
                GetComponent<ExplosiveController>().Explode();
			Destroy(gameObject);		
		}
	}
	
}

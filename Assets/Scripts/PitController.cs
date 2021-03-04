using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitController : MonoBehaviour
{
    public int pitDamage;
    [Range(.5f, 2f)]
    public float placementRange;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Health>().TakeDamage(pitDamage);
            
            if ((transform.position.x > col.gameObject.GetComponent<PlayerMovement>().lastGroundedPosition.x)&&
                (transform.position.x-col.gameObject.GetComponent<PlayerMovement>().lastGroundedPosition.x)>1)
            {
                col.gameObject.transform.position = new Vector2(col.gameObject.GetComponent<PlayerMovement>().lastGroundedPosition.x,
                    col.gameObject.GetComponent<PlayerMovement>().lastGroundedPosition.y);
            }
            else if(transform.position.x > col.gameObject.GetComponent<PlayerMovement>().lastGroundedPosition.x)
            {
                col.gameObject.transform.position = new Vector2(col.gameObject.GetComponent<PlayerMovement>().lastGroundedPosition.x - placementRange,
                    col.gameObject.GetComponent<PlayerMovement>().lastGroundedPosition.y);
            }
            else if ((transform.position.x <= col.gameObject.GetComponent<PlayerMovement>().lastGroundedPosition.x) &&
                (col.gameObject.GetComponent<PlayerMovement>().lastGroundedPosition.x-transform.position.x) > 1)
            {
                col.gameObject.transform.position = new Vector2(col.gameObject.GetComponent<PlayerMovement>().lastGroundedPosition.x,
                    col.gameObject.GetComponent<PlayerMovement>().lastGroundedPosition.y);
            }
            else if (transform.position.x <= col.gameObject.GetComponent<PlayerMovement>().lastGroundedPosition.x)
            {
                col.gameObject.transform.position = new Vector2(col.gameObject.GetComponent<PlayerMovement>().lastGroundedPosition.x + placementRange,
                    col.gameObject.GetComponent<PlayerMovement>().lastGroundedPosition.y);
            }

        }
    }
}

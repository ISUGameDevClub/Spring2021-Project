using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public bool isPlayer;
    public int damage;
    public List<GameObject> AttackedList = new List<GameObject>(0);



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((collision.gameObject.tag == "Player" && !isPlayer) || (collision.gameObject.tag == "Enemy" && isPlayer))
        {
            Health he = collision.gameObject.GetComponent<Health>();
            if (he != null)
            {
               // GameObject o = AttackedList.Find((e) => e.name == collision.gameObject.name);
                //if (o == null)
                {
                    he.TakeDamage(damage);
                    //AttackedList.Add(collision.gameObject);
                }
            }

        }
    }

    public void ListReset()
    {
        if(AttackedList != null)
        {
            AttackedList.Clear();
        }

    }
}

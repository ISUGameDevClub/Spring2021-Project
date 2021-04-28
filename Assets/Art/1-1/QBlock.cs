using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBlock : MonoBehaviour
{
    public GameObject ots;
    bool done;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!done && collision.gameObject.tag == "Player" && collision.gameObject.transform.position.y < transform.position.y - 1)
        {
            done = true;
            Instantiate(ots, transform.position + Vector3.up * .9f, new Quaternion(0, 0, 0, 0));
            GetComponent<AudioSource>().Play();
            if(GetComponent<Animator>())
                GetComponent<Animator>().SetTrigger("Used");
        }
    }
}

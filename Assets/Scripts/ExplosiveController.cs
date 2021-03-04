using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveController : MonoBehaviour
{
    public GameObject blastZone;
    public float timeBeforeExplosion;
    public float blastRadius;


    private HurtBox hurt;
    private Collider2D coll;
    private void Start()
    {
        coll = GetComponent<Collider2D>();
        blastZone.transform.localScale = new Vector3(blastRadius, blastRadius / 2, 1);
        hurt = blastZone.GetComponent<HurtBox>();
    }
    void Update()
    {
        if (GetComponent<Health>() != null && GetComponent<Health>().curHealth <= 0)
        {
            StartCoroutine(BlastActivate());
        }
    }
    private IEnumerator BlastActivate()
    {
        coll.enabled = false;
        coll.enabled = true;
        yield return new WaitForSeconds(timeBeforeExplosion);
        blastZone.SetActive(true);
    }
}

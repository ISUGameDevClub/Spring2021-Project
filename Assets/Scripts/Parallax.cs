using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public bool playerIndependent;
    public float size;
    public float parallaxSpeed;

    private float startpos;
    private GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.localPosition.x;
        cam = FindObjectOfType<CameraController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIndependent)
        {
            transform.Translate(Vector2.left * parallaxSpeed * Time.deltaTime);
            if (transform.localPosition.x < -size)
                transform.localPosition = new Vector3(size, transform.localPosition.y, 10);
            else if (transform.localPosition.x > size)
                transform.localPosition = new Vector3(-size, transform.localPosition.y, 10);
        }
        else
        {
            float temp = (cam.transform.position.x * (1 - parallaxSpeed));
            float dist = (cam.transform.position.x * parallaxSpeed);

            if(dist + startpos > 0)
                transform.localPosition = new Vector3((-((dist + startpos) % 72) + 36), transform.localPosition.y, 10);
            else
                transform.localPosition = new Vector3((-((dist + startpos) % 72) - 36), transform.localPosition.y, 10);
        }
    }
}
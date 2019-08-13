using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    private GameObject target;
    public float i;
    private float radius = 4.5f;

    public bool isStreaming;

    void Start()
    {
        target = GameObject.Find("Rotator");
        
    }

    void Update()
    {
        
        //float newZ = transform.eulerAngles.z + target.transform.eulerAngles.z;
        Vector2 pointing = new Vector2(transform.position.x, transform.position.y);
        transform.up = pointing;

        float ang = (i * Mathf.PI * 2f / Level.enemies) + (target.transform.eulerAngles.z * (Mathf.PI / 180));
        Vector3 newPos = new Vector3(Mathf.Cos(ang) * radius + target.transform.position.x, Mathf.Sin(ang) * radius + target.transform.position.y, 0);
        transform.position = newPos;
    }
}

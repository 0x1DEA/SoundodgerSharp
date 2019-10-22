using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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
        transform.up = new Vector2(transform.position.x, transform.position.y);

        float ang = (i * Mathf.PI * 2f / Level.enemies) + (target.transform.eulerAngles.z * (Mathf.PI / 180));
        transform.position = new Vector3(Mathf.Cos(ang) * radius + target.transform.position.x, Mathf.Sin(ang) * radius + target.transform.position.y, 0); ;
    }
}

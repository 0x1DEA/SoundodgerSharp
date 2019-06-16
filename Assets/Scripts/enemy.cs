using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    private GameObject target;
    private float angle;
    public float arenaRadius;
    public float enemyRadius;
    public Vector3 center;
    public float i;
    private float radius = 4.5f;

    void Start()
    {
        
    }

    void Update()
    {
        target = GameObject.Find("Enemies");
        //float newZ = transform.eulerAngles.z + target.transform.eulerAngles.z;

        float ang = (i * Mathf.PI * 2f / game.numObjects) + target.transform.eulerAngles.z;
        Vector3 newPos = new Vector3(Mathf.Cos(ang) * radius, Mathf.Sin(ang) * radius, 0);
        transform.position = newPos;
        //angle = transform.eulerAngles.z + target.transform.eulerAngles.z;
        //transform.position = new Vector3(center.x + Mathf.Sin(angle) * (arenaRadius + enemyRadius), center.y + Mathf.Cos(angle) * (arenaRadius + enemyRadius), 0);
    }
}

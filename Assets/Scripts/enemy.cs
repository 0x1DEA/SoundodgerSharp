using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    private GameObject target;
    public float i;
    private float radius = 4.5f;

    void Start()
    {
        target = GameObject.Find("Enemies");
        
    }

    void Update()
    {
        
        //float newZ = transform.eulerAngles.z + target.transform.eulerAngles.z;
        Vector2 pointing = new Vector2(transform.position.x, transform.position.y);
        transform.up = pointing;

        float ang = (i * Mathf.PI * 2f / game.enemies) + (target.transform.eulerAngles.z * (Mathf.PI / 180));
        Vector3 newPos = new Vector3(Mathf.Cos(ang) * radius, Mathf.Sin(ang) * radius, 0);
        transform.position = newPos;
    }
}

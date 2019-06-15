using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    // Bullet Prefabs
    public GameObject linA;
    public GameObject linB;
    public GameObject bub;
    public GameObject homing;
    public GameObject hug;
    public GameObject heart;

    private GameObject target;

    void Start()
    {
        
    }

    void Update()
    {
        target = GameObject.Find("Enemies");
        float newZ = transform.eulerAngles.z + target.transform.eulerAngles.z;
        transform.rotation = Quaternion.Euler(0f, 0f, newZ);

        if (Input.GetKeyDown(KeyCode.Space)) {
            Instantiate(linA, new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f));
        }
    }
}

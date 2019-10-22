using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private Vector3 cam;

    void Start()
    {
        
    }

    void Update()
    {
        cam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(cam.x, cam.y, 0);
        transform.Rotate(Vector3.forward * -2);
    }
}
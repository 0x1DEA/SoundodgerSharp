using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    //constrain (thanks viri!!!)
    public float arenaRadius;
    public float playerRadius;
    private Vector3 cam;
    private float playerY;
    private float playerX;
    private float angle;
    private Vector3 center;

    void Start()
    {
        center = new Vector3(0, 0, 0);
    }

    void Update()
    {

        cam = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(cam.x, cam.y, 0);

        playerY = transform.position.y;
        playerX = transform.position.x;
        if (Mathf.Sqrt(Mathf.Pow(center.y - playerY, 2) + Mathf.Pow(center.x - playerX, 2)) > arenaRadius - playerRadius) {
            angle = Mathf.Atan2(playerX - center.x, playerY - center.y);
            transform.position = new Vector3(center.x + Mathf.Sin(angle) * (arenaRadius - playerRadius), center.y + Mathf.Cos(angle) * (arenaRadius - playerRadius), 0);
        }
    }
}
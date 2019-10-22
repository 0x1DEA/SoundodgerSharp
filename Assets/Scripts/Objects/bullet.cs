using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // XML data
    public float offset;
    public bool playerAimed;
    public float speed;

    // Logic
    private float arenaRadius = 4.2f;
    private float bulletRadius = 0.2f;
    private float life;
    private bool insideArena = false;

    void Start()
    {
        if (playerAimed)
        {
            float pointing = Mathf.Atan2(Level.player.transform.position.y, Level.player.transform.position.x) * Mathf.Rad2Deg;
            transform.up = Level.player.transform.position - transform.position;
        }
        else
        {
            Vector2 pointing = new Vector2(-transform.position.x, -transform.position.y);
            transform.up = pointing;
        }

        transform.Rotate(0, 0, offset);

        GetComponentInChildren<SpriteRenderer>().material.color = Level.color[0];
    }

    void Update()
    {
        life += 0.1f;

        transform.position += transform.TransformDirection(new Vector2(0, speed * Level.timeWarp));
        
        if (Mathf.Sqrt(Mathf.Pow(transform.position.y, 2) + Mathf.Pow(transform.position.x, 2)) < arenaRadius - bulletRadius)
        {
            insideArena = true;
        }
        if (((Mathf.Sqrt(Mathf.Pow(transform.position.y, 2) + Mathf.Pow(transform.position.x, 2)) > arenaRadius - bulletRadius) && insideArena == true) || !insideArena && life > 10f)
        {
            Destroy(gameObject);
        }
    }
}
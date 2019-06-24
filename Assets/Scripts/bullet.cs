using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private float arenaRadius = 4.2f;
    private float bulletRadius = 0.2f;
    private float life;
    private bool insideArena = false;

    // XML data
    public float offset;
    public bool playerAimed;
    public float speed = 0.05f; 

    // Start is called before the first frame update
    void Start()
    {
        
        if (playerAimed) {

            float pointing = Mathf.Atan2(game.player.transform.position.y, game.player.transform.position.x) * Mathf.Rad2Deg;
            transform.up = game.player.transform.position - transform.position;
        } else {

            Vector2 pointing = new Vector2(-transform.position.x, -transform.position.y);
            transform.up = pointing;
        }
        
        transform.Rotate(0, 0, offset);
    }

    // Update is called once per frame
    void Update()
    {
        life += 0.1f;

        transform.position += transform.TransformDirection((Vector3.up) * speed * game.timeWarp);

        float playerY = transform.position.y;
        float playerX = transform.position.x;
        if (Mathf.Sqrt(Mathf.Pow(playerY, 2) + Mathf.Pow(playerX, 2)) < arenaRadius - bulletRadius) {
            insideArena = true;
        }
        if ((Mathf.Sqrt(Mathf.Pow(playerY, 2) + Mathf.Pow(playerX, 2)) > arenaRadius - bulletRadius) && insideArena == true) {
            Destroy(gameObject);
        }
        if (transform.position.x > 8.5 || transform.position.x < -8.5 || transform.position.y > 5.5 || transform.position.y < -5.5) {
            Destroy(gameObject);
        }
    }
}
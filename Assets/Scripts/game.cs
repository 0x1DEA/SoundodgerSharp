using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class game : MonoBehaviour
{

    // Level XML Variables
    public static float timeWarp = 1f;
    public static float spinRate = 0f;

    public float radius;
    public GameObject enemy;
    public GameObject ring;
    public Quaternion rot;
    public static GameObject player;
    private int iframe;
    public AudioSource source;

    public float arenaRadius;
    public float enemyRadius;
    public Vector3 center;

    void Start() {
        SpawnEnemies();
        player = GameObject.Find("Player");
        source = GetComponent<AudioSource>();
        source.Play();
    }

    void Update()
    {
        iframe++;
        if (iframe > 20) {
            MakeRing();
            iframe = 0;
        }
        level.checkMarkers();
    }

    void SpawnEnemies() {

        for (int i = 0; i < level.enemies; i++) {
            float ang = i * Mathf.PI * 2f / level.enemies;
            Vector3 newPos = new Vector3(Mathf.Cos(ang) * radius, Mathf.Sin(ang) * radius, 0);
            GameObject enemyInstance = Instantiate(enemy, newPos, Quaternion.identity);
            enemyInstance.gameObject.name = (i + 1).ToString();
            enemy enemyObject = enemyInstance.GetComponent<enemy>();
            enemyObject.i = i + 1;
        }
    }

    void MakeRing() {

        Instantiate(ring, new Vector3(0, 0, 0), rot);
    }
}

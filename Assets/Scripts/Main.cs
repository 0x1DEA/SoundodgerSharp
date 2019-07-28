using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Main : MonoBehaviour
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
    public static AudioSource source;

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
        if (Input.GetKeyDown(KeyCode.R)) {
            int bulletIteration = 0;
            foreach (level.marker bullet in level.bulletStructs) {
                level.bulletStructs[bulletIteration].fired = false;
                bulletIteration++;
            }
            int warpIteration = 0;
            foreach (level.marker warp in level.warpStructs) {
                level.warpStructs[warpIteration].fired = false;
                warpIteration++;
            }

            source.time = 0;
        }

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

    /*public static int HexToDec(string HexDec) {
        int dec = level.color[0].ToString("X");
        return dec;
    }

    public static float HexToFloat(string hexDec) {
        return

    }
    public static Color HexToColor(string hexDec) {
        float red;
        float green;
        float blue;
        return new Color(red, green, blue);
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game : MonoBehaviour
{
    // Bullet Prefabs
    public GameObject linA;
    public GameObject linB;
    public GameObject bub;
    public GameObject homing;
    public GameObject hug;
    public GameObject heart;

    // Level Variables
    public static float timeWarp = 1f;
    public static float spinRate = 0f;
    public static float enemies = 10f;

    public float radius;
    public GameObject enemy;
    public GameObject ring;
    public Quaternion rot;
    public static GameObject player;
    private int iframe;

    public float arenaRadius;
    public float enemyRadius;
    public Vector3 center;

    AudioSource audioData;

    void Start()
    {
        SpawnEnemies();
        player = GameObject.Find("Player");
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
        timeWarp = Mathf.Clamp(timeWarp, -10.0f, 10.0f);
    }

    void Update()
    {
        iframe++;
        if (iframe > 20) {
            MakeRing();
            iframe = 0;
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            spinRate = Mathf.SmoothStep(spinRate, spinRate - 0.5f, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            spinRate = Mathf.SmoothStep(spinRate, spinRate + 0.5f, 0.5f);
        }
        timeWarp += Input.GetAxis("Mouse ScrollWheel") * 5f;

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SpawnBullet(linA, "1", 0f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SpawnBullet(linA, "2", 0f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SpawnBullet(linA, "3", 0f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            SpawnBullet(linA, "4", 0f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            SpawnBullet(linA, "5", 0f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            SpawnBullet(linA, "6", 0f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7)) {
            SpawnBullet(linA, "7", 0f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8)) {
            SpawnBullet(linA, "8", 0f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            SpawnBullet(linA, "9", 0f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            SpawnBullet(linA, "10", 0f);
        }
    }
    
    void SpawnEnemies() {

        for (int i = 0; i < enemies; i++) {
            float ang = i * Mathf.PI * 2f / enemies;
            Vector3 newPos = new Vector3(Mathf.Cos(ang) * radius, Mathf.Sin(ang) * radius, 0);
            GameObject enemyInstance = Instantiate(enemy, newPos, Quaternion.identity);
            enemyInstance.gameObject.name = (i + 1).ToString();
            enemy enemyObject = enemyInstance.GetComponent<enemy>();
            enemyObject.i = i + 1;
        }
    }

    public void SpawnBullet(GameObject bulletType, string enemyNum, float offset) {

        GameObject enemyObject = GameObject.Find(enemyNum.ToString());
        Vector3 enemyPosition = enemyObject.transform.position;
        GameObject bulletInstance = Instantiate(bulletType, enemyPosition, new Quaternion(0f, 0f, 0f, 0f));
        bullet bulletObject = bulletInstance.GetComponent<bullet>();
        bulletObject.offset = offset;
        GameObject selectedEnemy = GameObject.Find(enemyNum);
        selectedEnemy.transform.Find("flash").GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
    }
    void MakeRing() {

        Instantiate(ring, new Vector3(0, 0, 0), rot);
    }
}

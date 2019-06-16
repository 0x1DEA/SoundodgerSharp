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

    public static float numObjects = 10f;
    public float radius;
    public GameObject enemy;
    public GameObject ring;
    public Quaternion rot;
    private int iframe = 0;

    public float arenaRadius;
    public float enemyRadius;
    public Vector3 center;

    void Start()
    {
        SpawnEnemies();
    }

    void Update()
    {
        iframe++;
        if (iframe == 20) {
            Instantiate(ring, new Vector3(0, 0, 0), rot);
            iframe = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            SpawnBullet(linA, "1");
        }
    }
    
    void SpawnEnemies() {

        /*for (int i = 0; i < numObjects; i++) {

            // Set enemy number to be i + 1 (because numbers start from 0 but enemy count starts from 1)
            int i2 = i + 1;

            // Divide circle by number of enemies
            float ang = i * (360 / numObjects);
            Debug.Log("ang: " + (i * (360 / numObjects)).ToString());

            // Set rotation to 0
            Quaternion rot = Quaternion.Euler(0, 0, ang);

            // Enemy position
            // Instead of subtracting, I added the radii so they would be on the outside instead of inside
            // Same positioning used for enemy but instead, mouse position is actually the origin. So the angle
            Vector3 enemyPos = new Vector3(center.x + Mathf.Sin(ang) * (arenaRadius + enemyRadius), center.y + Mathf.Cos(ang) * (arenaRadius + enemyRadius), 0);

            // Spawn instance and store in enemyInstance
            GameObject enemyInstance = Instantiate(enemy, enemyPos, rot);
            
            // Set name to enemy number
            enemyInstance.gameObject.name = i2.ToString();
        }*/

        for (int i = 0; i < numObjects; i++) {
            float ang = i * Mathf.PI * 2f / numObjects;
            Vector3 newPos = new Vector3(Mathf.Cos(ang) * radius, Mathf.Sin(ang) * radius, 0);
            GameObject enemyInstance = Instantiate(enemy, newPos, Quaternion.identity);
            enemyInstance.gameObject.name = (i + 1).ToString();
            enemy enemyObject = enemyInstance.GetComponent<enemy>();
            enemyObject.i = i;
        }
    }

    void SpawnBullet(GameObject bulletType, string enemyNum) {
        GameObject enemyObject = GameObject.Find(enemyNum.ToString());
        Vector3 enemyPosition = enemyObject.transform.position;
        Instantiate(bulletType, enemyPosition, new Quaternion(0f, 0f, 0f, 0f));
    }
}

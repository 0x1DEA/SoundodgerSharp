using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject enemy;
    public GameObject ring;
    private int iframe;

    void Start()
    {
        spawnEnemies();
    }

    void Update()
    {
        iframe++;
        if (iframe > 30)
        {
            makeRing();
            iframe = 0;
        }

        Level.checkMarkers();
    }

    void spawnEnemies()
    {
        for (int i = 0; i < Level.enemies; i++)
        {
            GameObject enemyInstance = Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
            enemyInstance.gameObject.name = (i + 1).ToString();
            Enemy enemyObject = enemyInstance.GetComponent<Enemy>();
            enemyObject.i = i + 1;
        }
    }

    void makeRing()
    {
        Instantiate(ring, new Vector3(0, 0, 0), gameObject.transform.rotation);
    }
}

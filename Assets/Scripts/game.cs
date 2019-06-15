using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game : MonoBehaviour
{

    public float numObjects = 10f;
    public float radius;
    public GameObject enemy;
    public GameObject ring;
    public Quaternion rot;
    private int iframe = 0;

    void Start()
    {
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        iframe++;
        if (iframe == 20) {
            Instantiate(ring, new Vector3(0, 0, 0), rot);
            iframe = 0;
        }
    }
    
    void SpawnEnemies() {
        for (int i = 0; i < numObjects; i++) {
            float ang = i * (360 / (numObjects + 1));
            Quaternion rot = Quaternion.Euler(0, 0, ang);
            Instantiate(enemy, new Vector3(0, 0, 0), rot);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour {
    // Bullet Prefabs
    public static GameObject linA;
    public static GameObject linB;
    public static GameObject bub;
    public static GameObject homing;
    public static GameObject hug;
    public static GameObject heart;

    private void Start() {
        linA = (GameObject)Resources.Load("prefabs/Linear A", typeof(GameObject));
        linB = (GameObject)Resources.Load("prefabs/Linear B", typeof(GameObject));
        bub = (GameObject)Resources.Load("prefabs/Bubble", typeof(GameObject));
        homing = (GameObject)Resources.Load("prefabs/Homing", typeof(GameObject));
        hug = (GameObject)Resources.Load("prefabs/Hug", typeof(GameObject));
        heart = (GameObject)Resources.Load("prefabs/Heart", typeof(GameObject));
    }

    public class pattern {

        public static void normal() {

        }

        public static void wave() {

        }

        public static void stream() {

        }

        public static void burst() {

        }
    }

    public static void SpawnBullet(GameObject bulletType, int enemyNum = 1, float offset = 0) {

        GameObject enemyObject = GameObject.Find(enemyNum.ToString());
        enemyObject.transform.Find("flash").GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
        Vector3 enemyPosition = enemyObject.transform.position;
        GameObject bulletInstance = Instantiate(bulletType, enemyPosition, new Quaternion(0f, 0f, 0f, 0f));
        bullet bulletObject = bulletInstance.GetComponent<bullet>();
        bulletObject.offset = offset;
    }
}

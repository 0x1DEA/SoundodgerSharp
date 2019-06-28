using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour {
    // Bullet Prefabs
    static GameObject linA;
    static GameObject linB;
    static GameObject bub;
    static GameObject homing;
    static GameObject hug;
    static GameObject heart;

    static GameObject bulletType;
    static bool playerAimed;

    // Find all our bullet prefabs and search then ONCE to use for the future
    private void Start() {
        linA = (GameObject)Resources.Load("prefabs/Linear A", typeof(GameObject));
        linB = (GameObject)Resources.Load("prefabs/Linear B", typeof(GameObject));
        bub = (GameObject)Resources.Load("prefabs/Bubble", typeof(GameObject));
        homing = (GameObject)Resources.Load("prefabs/Homing", typeof(GameObject));
        hug = (GameObject)Resources.Load("prefabs/Hug", typeof(GameObject));
        heart = (GameObject)Resources.Load("prefabs/Heart", typeof(GameObject));
    }

    // Class for bullet patterns
    public class pattern {

        public static void normal(string _aim, string _bulletType, float _offset, int _amount, float _speed, float _coneSize, int[] _enemies) {
            if (_amount < 0) {
                // just spawn the flash
            } else {
                float angle = _coneSize / _amount;
                for (int i = 0; i < _amount; i++) {
                    SpawnBullet("nrm", _enemies, _offset + (angle * (i + 1)) - (_coneSize / 2), _aim);
                }
            }
        }

        public static void wave() {

        }

        public static void stream() {

        }

        public static void burst() {

        }
    }

    // Make bullets, don't call directly, for use by pattern class methods
    public static void SpawnBullet(string _bulletType, int[] _enemyNum, float _offset = 0, string _aim = "mid") {

        switch(_bulletType) {
            case "nrm":
                bulletType = linA;
                break;
            case "nrm2":
                bulletType = linB;
                break;
            case "bubble":
                bulletType = bub;
                break;
            case "hug":
                bulletType = hug;
                break;
            case "homing":
                bulletType = homing;
                break;
            case "heart":
                bulletType = heart;
                break;
            default:
                bulletType = linA;
                break;

        }
        if (_aim == "pl") {
            playerAimed = true;
        }
        for (int i = 0; i < _enemyNum.Length; i++) {
            GameObject enemyObject = GameObject.Find((_enemyNum[i] + 1).ToString());
            enemyObject.transform.Find("flash").GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
            Vector3 enemyPosition = enemyObject.transform.position;
            GameObject bulletInstance = Instantiate(bulletType, enemyPosition, new Quaternion(0f, 0f, 0f, 0f));
            bullet bulletObject = bulletInstance.GetComponent<bullet>();
            bulletObject.offset = _offset;
            bulletObject.playerAimed = playerAimed;
        }
    }
}

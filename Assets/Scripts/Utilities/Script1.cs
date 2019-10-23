using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    // Bullet Prefabs
    static GameObject linA;
    static GameObject linB;
    static GameObject bub;
    static GameObject homing;
    static GameObject hug;
    static GameObject heart;

    static GameObject bulletType;

    // Find all our bullet prefabs and search then ONCE to use for the future
    private void Start()
    {
        linA = (GameObject)Resources.Load("prefabs/Linear A", typeof(GameObject));
        linB = (GameObject)Resources.Load("prefabs/Linear B", typeof(GameObject));
        bub = (GameObject)Resources.Load("prefabs/Bubble", typeof(GameObject));
        homing = (GameObject)Resources.Load("prefabs/Homing", typeof(GameObject));
        hug = (GameObject)Resources.Load("prefabs/Hug", typeof(GameObject));
        heart = (GameObject)Resources.Load("prefabs/Heart", typeof(GameObject));
    }

    // Class for bullet patterns
    public class Pattern
    {
        // Spawn a normal bullet
        public static void normal(bool _aim, int _bulletType, float _offset, int _amount, float _speed, float _coneSize, int[] _enemies)
        {
            if (_amount < 1)
            {
                // just spawn the flash
            }
            else
            {
                // Divide the total cone angle into indiviual angles to cycle through
                float angle = _coneSize / _amount;
                for (int i = 0; i < _amount; i++)
                {
                    SpawnBullet(_bulletType, _enemies, _offset + (angle * (i + 1)) - (_coneSize / 2), _aim, _speed);
                }
            }
        }

        public static void wave(int _rows, bool _aim, int _bulletType, float _offset0, float _offset1, int _amount0, int _amount1, float _speed0, float _speed1, float _coneSize0, float _coneSize1, int[] _enemies)
        {
            if (_amount0 < 1)
            {
                // just spawn the flash
            }
            else
            {
                for (int i = 0; i < _rows; i++)
                {
                    float angle = Mathf.Lerp(_coneSize0, _coneSize1, (1f / _rows) * (i + 1)) / Mathf.Lerp(_amount0, _amount1, (1f / _rows) * (i + 1));
                    for (int ii = 0; ii < Mathf.Lerp(_amount0, _amount1, (1f / _rows) * (i + 1)); ii++)
                    {
                        SpawnBullet(_bulletType, _enemies, Mathf.Lerp(_offset0, _offset1, (1f / _rows) * (i + 1)) + (angle * (ii + 1)) - (_coneSize0 / 2), _aim, Mathf.Lerp(_speed0, _speed1, (1f / _rows) * (i + 1)));
                    }
                }
            }
        }

        public static void stream()
        {

        }

        public static void burst(bool _aim, int _bulletType, float _offset, int _amount, float _speed0, float _speed1, float _coneSize, int[] _enemies)
        {
            if (_amount < 0)
            {
                // just spawn the flash
            }
            else
            {
                System.Random random = new System.Random();
                float angle = _coneSize / _amount;
                for (int i = 0; i < _amount; i++)
                {
                    // Generate a random speed and cone size
                    double sprandom = System.Math.Round(random.NextDouble() * (_speed1 - _speed0) + _speed0, 1);
                    double conerandom = System.Math.Round(random.NextDouble() * (_coneSize - 1) + 1, 1);

                    // Once we have gotten or generated all needed values, we can spawn our bullet
                    SpawnBullet(_bulletType, _enemies, _offset + (float)conerandom, _aim, (float)sprandom);
                }
            }
        }
    }

    public static void flash(int _bulletType, int _enemyNum)
    {
        if (_bulletType == 4)
        {
            _bulletType = 8;
        }
        GameObject.Find(_enemyNum.ToString()).transform.Find("flash").GetComponent<Renderer>().material.color = Level.color[_bulletType];
    }

    // Makes a bullet should only be called via pattern methods
    public static void SpawnBullet(int _bulletType, int[] _enemyNum, float _offset = 0, bool _aim = false, float _speed = 1.0f)
    {
        // Set bullet type to it's GameObject based on it's type ID
        switch (_bulletType)
        {
            case 0:
                bulletType = linA;
                break;
            case 1:
                bulletType = linB;
                break;
            case 2:
                bulletType = bub;
                break;
            case 3:
                bulletType = homing;
                break;
            case 4:
                bulletType = hug;
                break;
            case 5:
                bulletType = heart;
                break;
        }

        // Initiates the bullet launch sequence for each enemy around the arena
        for (int i = 0; i < _enemyNum.Length; i++)
        {
            // Find the bullet's parent enemy
            GameObject enemyObject = GameObject.Find((_enemyNum[i]).ToString());
            // Flash enemy
            flash(_bulletType, _enemyNum[i]);
            // Creates a new bullet where the enemy is
            GameObject bulletInstance = Instantiate(bulletType, enemyObject.transform.position, new Quaternion(0f, 0f, 0f, 0f));
            // Find instance into a variable and assign it it's XML attributes
            Bullet bulletObject = bulletInstance.GetComponent<Bullet>();
            bulletObject.offset = _offset;
            bulletObject.playerAimed = _aim;
            bulletObject.speed = _speed / 175f;
        }
    }
}

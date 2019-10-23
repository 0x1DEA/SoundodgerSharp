using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // Level Attributes
    public static string nick;
    public static string title;
    public static string artist;
    public static string designer;
    public static string mp3Name;
    public static string subtitle;
    public static float enemies;
    public static int difficulty;
    public static int preview;
    public static bool bgBlack;
    public static bool containsHeart;
    public static Color[] color = new Color[9];
    public static AudioSource song;

    // Logic
    public static int lastBullet = 0;
    public static int lastTimeWarp = 0;
    public static int lastSpinRate = 0;
    public static float timeWarp = 1f;
    public static float spinRate = 0;

    public static GameObject player;

    // Thing that XML gets parsed into
    public struct marker
    {
        public float time;
        public int[] enemies;
        // Spin = 0 Time = 1
        public int warpType;
        public float val;
        // normal = 0, wave = 1, stream = 2, burst = 3
        public int shotType;
        // nrm = 0, nrm2 = 1, bubble = 2, homing = 3, hug = 4, heart = 5
        public int bulletType;
        public int rows;
        public bool playerAimed;
        public int amount0;
        public float offset0;
        public float speed0;
        public float angle0;
        public int amount1;
        public float offset1;
        public float speed1;
        public float angle1;
        public float duration;
        public bool fired;
    }

    public static marker[] bullets;
    public static marker[] timeWarps;
    public static marker[] spinRates;

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    void Start()
    {
        song.Play();
        song = GetComponent<AudioSource>();
    }

    public static void sortWarps()
    {
        Array.Sort<marker>(timeWarps, (x, y) => x.time.CompareTo(y.time));
        Array.Sort<marker>(spinRates, (x, y) => x.time.CompareTo(y.time));
    }

    public static void checkMarkers()
    {
        for (int i = lastBullet; i < bullets.Length; i++)
        {
            if (bullets[i].fired || (bullets[i].time > song.time + Time.deltaTime))
            {
                lastBullet = i;
                i = bullets.Length;
            }
            else if (!bullets[i].fired && (bullets[i].time < song.time + Time.deltaTime))
            {
                switch (bullets[i].shotType)
                {
                    case 0:
                        Script.Pattern.normal(bullets[i].playerAimed, bullets[i].bulletType, bullets[i].offset0, bullets[i].amount0, bullets[i].speed0, bullets[i].angle0, bullets[i].enemies);
                        break;
                    case 1:
                        Script.Pattern.wave(bullets[i].rows, bullets[i].playerAimed, bullets[i].bulletType, bullets[i].offset0, bullets[i].offset1, bullets[i].amount0, bullets[i].amount1, bullets[i].speed0, bullets[i].speed1, bullets[i].angle0, bullets[i].angle1, bullets[i].enemies);
                        break;
                    case 2:
                        Script.Pattern.normal(bullets[i].playerAimed, bullets[i].bulletType, bullets[i].offset0, bullets[i].amount0, bullets[i].speed0, bullets[i].angle0, bullets[i].enemies);
                        break;
                    case 3:
                        Script.Pattern.burst(bullets[i].playerAimed, bullets[i].bulletType, bullets[i].offset0, bullets[i].amount0, bullets[i].speed0, bullets[i].speed1, bullets[i].angle0, bullets[i].enemies);
                        break;
                }
                bullets[i].fired = true;
            }
        }

        // Same as above but shorter and for timewarps
        for (int i = lastTimeWarp; i < timeWarps.Length; i++)
        {
            // Check for a TimeWarp that is fired or too early
            if (timeWarps[i].fired || (timeWarps[i].time > song.time + Time.deltaTime))
            {
                lastTimeWarp = i;
                i = timeWarps.Length;
            } else if (!timeWarps[i].fired && (timeWarps[i].time < song.time + Time.deltaTime))
            {
                timeWarp = timeWarps[i].val;
                timeWarps[i].fired = true;
            }
        }
        
        // Same as above but for spinrates
        for (int i = lastSpinRate; i < spinRates.Length; i++)
        {
            if (spinRates[i].fired || (spinRates[i].time > song.time + Time.deltaTime))
            {
                lastSpinRate = i;
                i = spinRates.Length;
            }
            else if (!spinRates[i].fired && (spinRates[i].time < song.time + Time.deltaTime))
            {
                spinRate = spinRates[i].val;
                spinRates[i].fired = true;
            }
        }
    }
}

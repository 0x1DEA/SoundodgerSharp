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

    // Keep seperate arrays of Bullets and Warps for some reason
    public static marker[] bullets;
    public static marker[] timeWarps;
    public static marker[] spinRates;

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    void Start()
    {
        // Start the song bababooey
        song = GetComponent<AudioSource>();
        song.Play();
    }

    public static void sortWarps()
    {
        // Sort each warp by time because they are checked in order
        Array.Sort<marker>(timeWarps, (x, y) => x.time.CompareTo(y.time));
        Array.Sort<marker>(spinRates, (x, y) => x.time.CompareTo(y.time));
    }

    // Runs every game update, fires patterns, and starts from the last fired marker in it's type
    public static void checkMarkers()
    {
        // For each bullet in array starting from last fired bullet
        for (int i = lastBullet; i < bullets.Length; i++)
        {
            // If bullet has been fired or it's too early, stop the loop
            if (bullets[i].fired || (bullets[i].time > song.time + Time.deltaTime))
            {
                lastBullet = i;
                i = bullets.Length;
            }
            // If it hasn't been fired and it's late, fire it
            else if (!bullets[i].fired && (bullets[i].time < song.time + Time.deltaTime))
            {
                // Shot type is represented by a number, call method for the correct pattern type
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

        // Start from last fired warp, search all remaining warps
        for (int i = lastTimeWarp; i < timeWarps.Length; i++)
        {
            // Check for a TimeWarp that is fired or too early
            if (timeWarps[i].fired || (timeWarps[i].time > song.time + Time.deltaTime))
            {
                // Set the last TimeWarp to this one
                lastTimeWarp = i;
                // End the loop here
                i = timeWarps.Length;
            } else if (!timeWarps[i].fired && (timeWarps[i].time < song.time + Time.deltaTime))
            {
                timeWarp = timeWarps[i].val;
                // Set TimeWarp to fired so it doesn't repeat
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

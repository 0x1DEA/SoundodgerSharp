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

    // Vars for logic
    public static int lastBullet;
    public static int lastWarp;
    public static int lastRate;
    public static float timeWarp;
    public static float spinRate;
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
    public static marker[] bulletStructs;
    public static marker[] warpStructs;
    public static marker[] rateStructs;

    void Awake()
    {
        // Reset values
        lastBullet = 0;
        lastWarp = 0;
        lastRate = 0;

        timeWarp = 1f;
        spinRate = 0f;
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
        // Sort the warps by time because time and spins are intermixed
        Array.Sort<marker>(warpStructs, (x, y) => x.time.CompareTo(y.time));
        Array.Sort<marker>(rateStructs, (x, y) => x.time.CompareTo(y.time));
    }

    // Runs every game update, fires patterns, and starts from the last fired marker in it's type
    public static void checkMarkers()
    {
        for (int i = lastBullet; i < bulletStructs.Length; i++)
        {
            if (bulletStructs[i].fired || (bulletStructs[i].time > song.time + Time.deltaTime))
            {
                lastBullet = i;
                i = bulletStructs.Length;
            }
            else if (!bulletStructs[i].fired && (bulletStructs[i].time < song.time + Time.deltaTime))
            {
                switch (bulletStructs[i].shotType)
                {
                    case 0:
                        Script.Pattern.normal(bulletStructs[i].playerAimed, bulletStructs[i].bulletType, bulletStructs[i].offset0, bulletStructs[i].amount0, bulletStructs[i].speed0, bulletStructs[i].angle0, bulletStructs[i].enemies);
                        break;
                    case 1:
                        Script.Pattern.wave(bulletStructs[i].rows, bulletStructs[i].playerAimed, bulletStructs[i].bulletType, bulletStructs[i].offset0, bulletStructs[i].offset1, bulletStructs[i].amount0, bulletStructs[i].amount1, bulletStructs[i].speed0, bulletStructs[i].speed1, bulletStructs[i].angle0, bulletStructs[i].angle1, bulletStructs[i].enemies);
                        break;
                    case 2:
                        Script.Pattern.normal(bulletStructs[i].playerAimed, bulletStructs[i].bulletType, bulletStructs[i].offset0, bulletStructs[i].amount0, bulletStructs[i].speed0, bulletStructs[i].angle0, bulletStructs[i].enemies);
                        break;
                    case 3:
                        Script.Pattern.burst(bulletStructs[i].playerAimed, bulletStructs[i].bulletType, bulletStructs[i].offset0, bulletStructs[i].amount0, bulletStructs[i].speed0, bulletStructs[i].speed1, bulletStructs[i].angle0, bulletStructs[i].enemies);
                        break;
                }
                bulletStructs[i].fired = true;
            }
        }

        // Start from last fired warp, search all remaining warps
        for (int i = lastWarp; i < warpStructs.Length; i++)
        {
            // Check for a TimeWarp that is fired or too early
            if (warpStructs[i].fired || (warpStructs[i].time > song.time + Time.deltaTime))
            {
                // Set the last TimeWarp to this one
                lastWarp = i;

                // End the loop here
                i = warpStructs.Length;
            } else if (!warpStructs[i].fired && (warpStructs[i].time < song.time + Time.deltaTime))
            {
                timeWarp = warpStructs[i].val;
                // Set TimeWarp to fired so it doesn't repeat
                warpStructs[i].fired = true;
            }
        }

        for (int i = lastRate; i < rateStructs.Length; i++)
        {
            // Check for a SpinRate that is fired or too early
            if (rateStructs[i].fired || (rateStructs[i].time > song.time + Time.deltaTime))
            {
                // Set the last SpinRate to this one
                lastRate = i;

                // End the loop here
                i = rateStructs.Length;
            }
            else if (!rateStructs[i].fired && (rateStructs[i].time < song.time + Time.deltaTime))
            {
                spinRate = rateStructs[i].val;
                // Set SpinRate to fired so it doesn't repeat
                rateStructs[i].fired = true;
            }
        }
    }
}
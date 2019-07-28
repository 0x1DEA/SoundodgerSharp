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
    public static GameObject player;

    // Thing that XML gets parsed into
    public struct marker {
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

    void Awake()
    {
        lastBullet = 0;
        player = GameObject.Find("Player");
    }

    /*
    ** Runs every game update, fires patterns
    ** Starts from the last fired marker in it's type
    */
    public static void checkMarkers()
    {
        int ii = 0;
        /*foreach (marker mark in bulletStructs)
        {
            if (!bulletStructs[iteration].fired && (bulletStructs[iteration].time < game.source.time + Time.deltaTime))
            {
                switch (bulletStructs[iteration].shotType)
                {
                    case 0:
                        script.pattern.normal(bulletStructs[iteration].playerAimed, bulletStructs[iteration].bulletType, bulletStructs[iteration].offset0, bulletStructs[iteration].amount0, bulletStructs[iteration].speed0, bulletStructs[iteration].angle0, bulletStructs[iteration].enemies);
                        break;
                    case 1:
                        script.pattern.wave(bulletStructs[iteration].rows, bulletStructs[iteration].playerAimed, bulletStructs[iteration].bulletType, bulletStructs[iteration].offset0, bulletStructs[iteration].offset1, bulletStructs[iteration].amount0, bulletStructs[iteration].amount1, bulletStructs[iteration].speed0, bulletStructs[iteration].speed1, bulletStructs[iteration].angle0, bulletStructs[iteration].angle1, bulletStructs[iteration].enemies);
                        break;
                    case 2:
                        //script.pattern.normal(bulletStructs[iteration].playerAimed, bulletStructs[iteration].bulletType, bulletStructs[iteration].offset0, bulletStructs[iteration].amount0, bulletStructs[iteration].speed0, bulletStructs[iteration].angle0, bulletStructs[iteration].enemies);
                        break;
                    case 3:
                        script.pattern.burst(bulletStructs[iteration].playerAimed, bulletStructs[iteration].bulletType, bulletStructs[iteration].offset0, bulletStructs[iteration].amount0, bulletStructs[iteration].speed0, bulletStructs[iteration].speed1, bulletStructs[iteration].angle0, bulletStructs[iteration].enemies);
                        break;
                }
                bulletStructs[i].fired = true;
            }
            i++;
        }*/
        for (int iteration = lastBullet; iteration < bulletStructs.Length; iteration++)
        {
            if (bulletStructs[iteration].fired || (bulletStructs[iteration].time > source.time + Time.deltaTime)) {
                lastBullet = iteration;
                iteration = bulletStructs.Length;
            } 
            else if (!bulletStructs[iteration].fired && (bulletStructs[iteration].time < source.time + Time.deltaTime))
            {
                switch (bulletStructs[iteration].shotType)
                {
                    case 0:
                        script.pattern.normal(bulletStructs[iteration].playerAimed, bulletStructs[iteration].bulletType, bulletStructs[iteration].offset0, bulletStructs[iteration].amount0, bulletStructs[iteration].speed0, bulletStructs[iteration].angle0, bulletStructs[iteration].enemies);
                        break;
                    case 1:
                        script.pattern.wave(bulletStructs[iteration].rows, bulletStructs[iteration].playerAimed, bulletStructs[iteration].bulletType, bulletStructs[iteration].offset0, bulletStructs[iteration].offset1, bulletStructs[iteration].amount0, bulletStructs[iteration].amount1, bulletStructs[iteration].speed0, bulletStructs[iteration].speed1, bulletStructs[iteration].angle0, bulletStructs[iteration].angle1, bulletStructs[iteration].enemies);
                        break;
                    case 2:
                        //script.pattern.normal(bulletStructs[iteration].playerAimed, bulletStructs[iteration].bulletType, bulletStructs[iteration].offset0, bulletStructs[iteration].amount0, bulletStructs[iteration].speed0, bulletStructs[iteration].angle0, bulletStructs[iteration].enemies);
                        break;
                    case 3:
                        script.pattern.burst(bulletStructs[iteration].playerAimed, bulletStructs[iteration].bulletType, bulletStructs[iteration].offset0, bulletStructs[iteration].amount0, bulletStructs[iteration].speed0, bulletStructs[iteration].speed1, bulletStructs[iteration].angle0, bulletStructs[iteration].enemies);
                        break;
                }
                bulletStructs[iteration].fired = true;
            }
        }
        foreach (marker warp in warpStructs)
        {
            if (!warp.fired && (warp.time < game.source.time + Time.deltaTime))
            {
                switch (warp.warpType)
                {
                    case 0:
                        game.spinRate = warp.val;
                        break;
                    case 1:
                        game.timeWarp = warp.val;
                        break;
                }
                warpStructs[ii].fired = true;
            }
            ii++;
        }
    }
}
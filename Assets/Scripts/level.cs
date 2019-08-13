using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
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
    public static float timeWarp = 1f;
    public static float spinRate = 0f;
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

    void Awake() {
        lastBullet = 0;
        lastWarp = 0;
        player = GameObject.Find("Player");
    }

    void Start() {
        song = GetComponent<AudioSource>();
        song.Play();
    }

    /*
    ** Runs every game update, fires patterns
    ** Starts from the last fired marker in it's type
    */
    public static void checkMarkers() {
        for (int bulletIteration = lastBullet; bulletIteration < bulletStructs.Length; bulletIteration++) {
            if (bulletStructs[bulletIteration].fired || (bulletStructs[bulletIteration].time > song.time + Time.deltaTime)) {
                lastBullet = bulletIteration;
                bulletIteration = bulletStructs.Length;
            } 
            else if (!bulletStructs[bulletIteration].fired && (bulletStructs[bulletIteration].time < song.time + Time.deltaTime)) {
                switch (bulletStructs[bulletIteration].shotType)
                {
                    case 0:
                        script.pattern.normal(bulletStructs[bulletIteration].playerAimed, bulletStructs[bulletIteration].bulletType, bulletStructs[bulletIteration].offset0, bulletStructs[bulletIteration].amount0, bulletStructs[bulletIteration].speed0, bulletStructs[bulletIteration].angle0, bulletStructs[bulletIteration].enemies);
                        break;
                    case 1:
                        script.pattern.wave(bulletStructs[bulletIteration].rows, bulletStructs[bulletIteration].playerAimed, bulletStructs[bulletIteration].bulletType, bulletStructs[bulletIteration].offset0, bulletStructs[bulletIteration].offset1, bulletStructs[bulletIteration].amount0, bulletStructs[bulletIteration].amount1, bulletStructs[bulletIteration].speed0, bulletStructs[bulletIteration].speed1, bulletStructs[bulletIteration].angle0, bulletStructs[bulletIteration].angle1, bulletStructs[bulletIteration].enemies);
                        break;
                    case 2:
                        // script.pattern.normal(bulletStructs[bulletIteration].playerAimed, bulletStructs[iteration].bulletType, bulletStructs[iteration].offset0, bulletStructs[iteration].amount0, bulletStructs[iteration].speed0, bulletStructs[iteration].angle0, bulletStructs[iteration].enemies);
                        break;
                    case 3:
                        script.pattern.burst(bulletStructs[bulletIteration].playerAimed, bulletStructs[bulletIteration].bulletType, bulletStructs[bulletIteration].offset0, bulletStructs[bulletIteration].amount0, bulletStructs[bulletIteration].speed0, bulletStructs[bulletIteration].speed1, bulletStructs[bulletIteration].angle0, bulletStructs[bulletIteration].enemies);
                        break;
                }
                bulletStructs[bulletIteration].fired = true;
            }
        }
        for (int warpIteration = lastWarp; warpIteration < warpStructs.Length; warpIteration++) {
            if (warpStructs[warpIteration].fired || (warpStructs[warpIteration].time > song.time + Time.deltaTime)) {
                lastWarp = warpIteration;
                warpIteration = warpStructs.Length;
            } else if (!warpStructs[warpIteration].fired && (warpStructs[warpIteration].time < song.time + Time.deltaTime)) {
                switch (warpStructs[warpIteration].warpType) {
                    case 0:
                        spinRate = warpStructs[warpIteration].val;
                        break;
                    case 1:
                        timeWarp = warpStructs[warpIteration].val;
                        break;
                }
                warpStructs[warpIteration].fired = true;
            }
        }
    }
}
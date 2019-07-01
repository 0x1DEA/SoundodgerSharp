using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level : MonoBehaviour {
    // Does C# have word arrays??
    public static string nick;
    public static string title;
    public static string artist;
    public static string designer;
    public static string mp3Name;
    public static string subtitle;
    
    public static float enemies;
    public static int difficulty;
    public static int preview;
    
    // Will move to an array (eventually)
    public static int[] color = new int[9];

    public struct marker {
        public float time;
        public int[] enemies;
        // Spin = 0 Time = 1
        public int warpType;
        public float val;
        // normal = 0, wave = 1, burst = 2, stream = 3
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

    public static marker[] bulletStructs;
    public static marker[] warpStructs;

    void Awake() {
        
    }
    public static void checkMarkers() {
        int i = 0;
        foreach(marker mark in bulletStructs){
            if(!mark.fired && (mark.time < Time.time + Time.deltaTime)) {
                Debug.Log(mark.time);
                switch(mark.shotType) {
                    case 1:

                        break;
                }
                bulletStructs[i].fired = true;
            }
            i++;
        }
    }
}
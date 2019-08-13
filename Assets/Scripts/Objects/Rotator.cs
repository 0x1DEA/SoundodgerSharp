using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    void Start()
    {
        
    }
    
    void Update() {
        transform.Rotate(0,0, Level.spinRate / 4f * Level.timeWarp);
    }
}

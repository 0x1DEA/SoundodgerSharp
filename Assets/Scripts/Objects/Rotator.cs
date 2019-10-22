using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float thing;

    void Update()
    {
        thing = Level.spinRate;
        gameObject.transform.Rotate(0, 0, thing / 4f * Level.timeWarp);
    }
}

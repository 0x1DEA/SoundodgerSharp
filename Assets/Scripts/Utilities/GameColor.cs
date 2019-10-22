using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameColor : MonoBehaviour
{
    public enum LevelColor
    {
        LinearA,
        LinearB,
        Homing,
        Bubble,
        Outlines,
        OuterRings,
        SlowMoBG,
        ScoreCircle,
        Hug
    };
    public LevelColor num;
    public bool parent;
    public bool cam;
    public bool arena;
    void Start()
    {
        if (parent)
        {
            GetComponentInChildren<SpriteRenderer>().material.color = Level.color[(int)num];
        }
        else if (!parent && !cam && !arena)
        {
            GetComponent<SpriteRenderer>().material.color = Level.color[(int)num];
        }
        else if (cam && Level.bgBlack)
        {
            GetComponent<Camera>().backgroundColor = new Color(0, 0, 0, 1);
        }
        else if ((arena || !cam) && Level.bgBlack)
        {
            GetComponent<SpriteRenderer>().material.color = new Color(0, 0, 0, 1);
        }
    }
}

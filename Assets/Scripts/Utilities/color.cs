using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color : MonoBehaviour
{
    public enum Enum
    {
        LinearA,
        LinearB,
        Homing,
        Bubble,
        Hug,
        Outlines,
        Outerrings,
        slowmobg,
        scorecircle
    };
    public Enum num;
    public bool parent;
    public bool cam;
    public bool arena;
    void Start()
    {
        if (parent)
        {
            GetComponentInChildren<SpriteRenderer>().material.color = level.color[(int)num];
        }
        else if (!parent && !cam && !arena)
        {
            GetComponent<SpriteRenderer>().material.color = level.color[(int)num];
        }
        else if (cam && level.bgBlack)
        {
            GetComponent<Camera>().backgroundColor = new Color(0, 0, 0, 1);
        }
        else if ((arena || !cam) && level.bgBlack)
        {
            GetComponent<SpriteRenderer>().material.color = new Color(0, 0, 0, 1);
        }
    }
}

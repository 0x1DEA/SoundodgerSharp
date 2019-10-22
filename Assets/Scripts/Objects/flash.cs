using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    private float alpha;

    void Start()
    {
        GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
        alpha = GetComponent<Renderer>().material.color.a;
    }

    void Update()
    {
        
        alpha = GetComponent<Renderer>().material.color.a - 0.01f;
        GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, alpha);

    }
}

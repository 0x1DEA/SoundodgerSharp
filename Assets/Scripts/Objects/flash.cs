using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flash : MonoBehaviour
{
    private float alpha;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
        alpha = GetComponent<Renderer>().material.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        
        alpha = GetComponent<Renderer>().material.color.a - 0.01f;
        GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, alpha);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    private int life;

    // Start is called before the first frame update
    void Start()
    {
        //transform.LookAt(new Vector3(0f,0f,0f));
    }

    // Update is called once per frame
    void Update()
    {
        life++;
    }
}

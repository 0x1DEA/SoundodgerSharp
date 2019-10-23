using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private float life = 0f;

    void Update()
    {
        life += 0.01f;
        transform.localScale += new Vector3(0.005f * life, 0.005f * life, 0);

        if (transform.localScale.x >= 3)
        {
            Destroy(gameObject);
        }
    }
}

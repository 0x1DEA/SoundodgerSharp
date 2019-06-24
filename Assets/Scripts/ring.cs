using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ring : MonoBehaviour
{
    private float life = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        life += 0.01f;
        transform.localScale += new Vector3(0.005f * life, 0.005f * life, 0);
        if (transform.localScale.x >= 3) {
            Destroy(gameObject);
        }
    }
}

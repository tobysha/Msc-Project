using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ttlSystem : MonoBehaviour
{
    public float ttl;

    void Start()
    {
        
    }


    void Update()
    {
        if(ttl>0)
        {
            ttl -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

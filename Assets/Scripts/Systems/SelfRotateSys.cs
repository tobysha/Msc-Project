using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotateSys : MonoBehaviour
{
    public float rotationSpeed = 10f; 
    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bulletMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 vector3 = transform.position;
        //this.gameObject.transform.position = new Vector3(vector3.x + 1f * Time.deltaTime, vector3.y + 0f * Time.deltaTime, 0f);
        this.gameObject.transform.position += gameObject.transform.right * Time.deltaTime;
    }
}

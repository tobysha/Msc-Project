using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_1_fireLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject firepoint;
    private bool youcanfire = false;
    private List<GameObject> emenys = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnColliderEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Monster")
        {
            emenys.Add(collision.gameObject);
        }
    }
    private void OnColliderExit(Collider collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            emenys.Remove(collision.gameObject);
        }
    }
    void fire()
    {

    }
}

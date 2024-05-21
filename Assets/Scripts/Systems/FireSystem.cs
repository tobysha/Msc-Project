using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSystem : MonoBehaviour
{
    public GameObject fire;
    private void Start()
    {
        //CreateFire();
    }
    public void CreateFire()
    {
        Instantiate(fire, this.transform.position, this.transform.rotation, this.gameObject.transform);
    }
}

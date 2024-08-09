using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarFireLogic : MonoBehaviour,IFire
{
    [SerializeField] private GameObject bullet;
    private void Start()
    {
    }
    public void OnFire(Transform target)
    {
        Instantiate(bullet, transform.position, Quaternion.identity, this.transform);
    }
    
}

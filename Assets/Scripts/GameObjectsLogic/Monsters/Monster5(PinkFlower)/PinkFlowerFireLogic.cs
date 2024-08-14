using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkFlowerFireLogic : MonoBehaviour,IFire
{
    [SerializeField]private GameObject[] BulletPoint;
    [SerializeField]private GameObject bullet;
    private ObjectsData objectsData;
    [SerializeField] private float speed = 1f;
    private void Start()
    {
        objectsData = GetComponent<ObjectsData>();
    }
    public void OnFire(Transform target)
    {
        foreach (var point in BulletPoint)
        {
            GameObject b = Instantiate(bullet, point.transform.position, point.transform.rotation);
            DamageSystem damageSystem = b.GetComponent<DamageSystem>();
            damageSystem.damage = objectsData.atk;
            b.GetComponent<Rigidbody2D>().velocity = point.transform.up * speed;
        }
    }
}

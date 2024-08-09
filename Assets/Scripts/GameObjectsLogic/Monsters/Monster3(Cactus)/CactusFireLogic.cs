using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CactusFireLogic : MonoBehaviour, IFire
{
    public GameObject bullet;
    public GameObject[] bulletCreate;
    public float speed = 1f; // 子弹的速度
    private float timer = 0f;
    public float Rate = 0.1f;
    private Transform Target;
    public void OnFire(Transform target)
    {
        Target = target;
        for(int i = 0; i< bulletCreate.Length; i++)
        {
            Invoke("bulletcreate", i * Rate);
        }
        //foreach (var bulletpoint in bulletCreate)//bullet create
        //{

        //    Vector3 direction = bulletpoint.transform.rotation * Vector3.up;
        //    GameObject bullet1 = Instantiate(bullet, transform.position, Quaternion.identity, bulletpoint.transform);
        //    bullet1.GetComponent<Rigidbody2D>().velocity = direction * speed;
        //    timer = Rate;

        //}
    }
    private void Update()
    {
        if(Target != null)
        {
            Vector3 direct = (Target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f); //turn slowly
        }
    }
    void bulletcreate()
    {
        foreach (var bulletpoint in bulletCreate)//bullet create
        {
            Vector3 direction = bulletpoint.transform.rotation * Vector3.up;
            GameObject bullet1 = Instantiate(bullet, bulletpoint.transform.position, Quaternion.identity);
            bullet1.GetComponent<Rigidbody2D>().velocity = direction * speed;
            timer = Rate;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneFireLogic : MonoBehaviour, IFire
{
    public GameObject bullet;
    private GameObject bulletCreate;
    public int bulletCount = 10; // 生成子弹的数量
    public float spreadAngle = 360f; // 子弹扩散的角度
    public float speed = 1f; // 子弹的速度
    private ObjectsData ObjectsData;
    private void Start()
    {
        ObjectsData = GetComponent<ObjectsData>();
    }
    public void OnFire(Transform target)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * spreadAngle / bulletCount;
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;
            GameObject bullet1 = Instantiate(bullet, transform.position, Quaternion.identity);
            bullet1.GetComponent<Rigidbody2D>().velocity = direction * speed;
            bullet1.GetComponent<DamageSystem>().damage = ObjectsData.atk;
        }

    }
}

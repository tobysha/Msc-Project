using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    enum bulletType
    {
        TOWERBULLET = 0,
        ENEMYBULLET = 1
    }
    public int damage;
    private ObjectsData objectdata;
    bulletType bt;
    private void Start()
    {
        objectdata = GetComponentInParent<ObjectsData>();
        damage = objectdata.atk;
        if (this.CompareTag("EnemyBullet"))
        {
            bt = bulletType.ENEMYBULLET;
        }
        else if (this.CompareTag("Towerbullet"))
        {
            bt = bulletType.TOWERBULLET;
        }
    }
    public int getDamage()
    {
        return damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(bt)
        {
            case bulletType.ENEMYBULLET:
                if (collision.gameObject.CompareTag("Tower"))
                {
                    collision.GetComponent<LifeSystem>().setHP(-damage);
                    Destroy(gameObject);
                }
                break;
            case bulletType.TOWERBULLET:
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    collision.GetComponent<LifeSystem>().setHP(-damage);
                    Destroy(gameObject);
                }
                break;
        }

    }
}

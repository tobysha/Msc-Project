using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    public int damage;
    public int getDamage()
    {
        return damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<LifeSystem>().setHP(-damage);
            Destroy(gameObject);
        }
    }
}

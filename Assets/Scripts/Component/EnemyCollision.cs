using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private FireSystem _fireSystem;
    private void Start()
    {
        _fireSystem = GetComponentInParent<FireSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tower"))
        {
            _fireSystem.enemies.Add(collision.gameObject);
            //LifeSystem ls = collision.gameObject.GetComponent<LifeSystem>();
            //ls.setHP(bullet.GetComponent<DamageSystem>().getDamage());
            //Debug.Log("youjinru");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tower") && _fireSystem.enemies.Count != 0)
        {
            if (_fireSystem.enemies.Contains(collision.gameObject))
            {
                _fireSystem.enemies.Remove(collision.gameObject);
            }
            //LifeSystem ls = collision.gameObject.GetComponent<LifeSystem>();
            //ls.setHP(bullet.GetComponent<DamageSystem>().getDamage());
        }
    }
}

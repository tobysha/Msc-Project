using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesWind : MonoBehaviour
{
    public float forceX = -1f;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Towerbullet") || collider.gameObject.CompareTag("EnemyBullet"))
        {
            Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D>();
            if (rb.velocity.x >= 0)
            {
                rb.AddForce(new Vector2(forceX, 0), ForceMode2D.Impulse);
            }
                
        }
    }

}

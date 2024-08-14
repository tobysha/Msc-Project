using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BanananaBulletDamage : MonoBehaviour
{

    [SerializeField] private int bounceTime;
    [SerializeField] private Sprite secondPNG;
    private SpriteRenderer spriteRenderer;
    private bulletMovement bulletmovement;
    private GameObject currentObject;
    public int damage;
    private void Start()
    {
        currentObject = this.gameObject;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        bulletmovement = GetComponent<bulletMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Tower"))
        {
            if(GetNextTarget(collision.gameObject)!=null && bounceTime > 0 && currentObject!= collision.gameObject)
            {
                collision.GetComponent<LifeSystem>().setHP(-damage);
                currentObject = collision.gameObject;
                spriteRenderer.sprite = secondPNG;
                bounceTime -= 1;
                bulletmovement.setTarget(GetNextTarget(collision.gameObject));
            }
            if(bounceTime==0|| GetNextTarget(collision.gameObject) == null)
            {
                Destroy(this.gameObject);
            }
        }
    }
    private Transform GetNextTarget(GameObject currentEnemy)
    {
        //Transform target = null;
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Tower");
        //GameObject[] TargetEnemies = new GameObject[enemies.Length];
        //for(int i = 0; i < enemies.Length; i++)
        //{
        //    if(enemies[i] != currentEnemy)
        //    {
        //        TargetEnemies[i] = enemies[i];
        //    }
        //}
        //float mindistance = 10000;
        //foreach (GameObject go in enemies)
        //{
        //    float currentDis = Vector3.Distance(this.gameObject.transform.position, go.transform.position);
        //    if (currentDis < mindistance)
        //    {
        //        target = go.transform;
        //        mindistance = currentDis;
        //    }
        //}
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Tower");
        GameObject[] targetEnemies = enemies.Where(enemy => enemy != currentEnemy).ToArray();
        Transform target = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in targetEnemies)
        {
            float distance = Vector3.Distance(this.transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                target = enemy.transform;
                minDistance = distance;
            }
        }
        return target;
    }
}

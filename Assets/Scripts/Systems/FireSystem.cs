using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FireSystem : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletCreate;
    public float CoolDowntime = 3f;
    public float countdown = 0f;

    private bool isShadow = false;
    public List<GameObject> enemies = new List<GameObject>();
    private void Start()
    {
        //countdown = 0f;
        //bulletCreate = GameObject.Find("BulletCreate"); ;
        //CreateFire();
        //AdjustTileZPosition();
    }

    private void Update()
    {
        countdown -= Time.deltaTime;
        if (enemies.Count != 0 && !isShadow)
        {
            CreateFire(getCloseEnemy());
        }
    }
    private Transform getCloseEnemy()
    {
        Transform transform = enemies[0].transform;
        float mindistance = Vector3.Distance(this.gameObject.transform.position, enemies[0].transform.position);
        foreach(GameObject go in enemies)
        {
            float currentDis = Vector3.Distance(this.gameObject.transform.position, enemies[0].transform.position);
            if (currentDis < mindistance)
            {
                transform = go.transform;
                mindistance = currentDis;
            }
        }
        return transform;
    }
    public void CreateFire(Transform tf)
    {
        if (countdown <= 0f)
        {
            Vector3 direction = (tf.position - transform.position).normalized;
           // Quaternion rotation = Quaternion.LookRotation(direction);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // 创建旋转四元数，只绕 Z 轴旋转
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            GameObject go = Instantiate(bullet, this.transform.position, rotation, bulletCreate.gameObject.transform);
            go.GetComponent<bulletMovement>().setTarget(tf);
            countdown = CoolDowntime;
        }
        
    }
    public void SetisShadow(bool s)
    {
        isShadow = s;
    }

}

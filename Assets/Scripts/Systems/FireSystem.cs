using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FireSystem : MonoBehaviour
{
    //public GameObject bullet;
    //public GameObject bulletCreate;
    private float countdown = 0f;

    private bool isShadow = false;
    private GameManager gameManager;
    private ObjectsData objectsData;

    public List<GameObject> enemies;

    private bool crazyMode = false;
    private bool crazyEnabled = false;

    
    private void Start()
    {
        countdown = 0f;
        //bulletCreate = GameObject.Find("BulletCreate"); 
        //CreateFire();
        //AdjustTileZPosition();
        enemies = new List<GameObject>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        objectsData = this.gameObject.GetComponent<ObjectsData>();
        InvokeRepeating("CrazyModeScan", 1f, 1f);
    }

    private void Update()
    {
        if(countdown>=0)
        {
            countdown -= Time.deltaTime;
        }
        if (enemies.Count != 0 && !isShadow)
        {
            Fire(this.gameObject);
        }
        CrazyModeActivate();

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
    private void CrazyModeScan()
    {
        if (gameManager.GetcurrentStage() == GameManager.GameStage.crazyStage && crazyMode == false && crazyEnabled == false)
        {
            crazyMode = true;
        }
    }
    private void CrazyModeActivate()
    {
        if (crazyMode == true)
        {
            crazyEnabled = true;
            objectsData.ChangeCoolDownTime(objectsData.GetCoolDowntime()/2);
            crazyMode = false;
        }

    }

    //public void CreateFire(Transform tf)
    //{
    //    if (countdown <= 0f)
    //    {
    //        Vector3 direction = (tf.position - transform.position).normalized;
    //       // Quaternion rotation = Quaternion.LookRotation(direction);
    //        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //        // 创建旋转四元数，只绕 Z 轴旋转
    //        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    //        GameObject go = Instantiate(bullet, this.transform.position, rotation, bulletCreate.gameObject.transform);
    //        go.GetComponent<bulletMovement>().setTarget(tf);
    //        countdown = objectsData.GetCoolDowntime();
    //    }
        
    //}
    public void SetisShadow(bool s)
    {
        isShadow = s;
    }
    public void Fire(GameObject fireObject)
    {
        //Debug.Log(this.gameObject.CompareTag("Tower"));
        if (fireObject.TryGetComponent<IFire>(out IFire fireable))
        {
            ///Debug.Log("111");
            if (countdown <= 0f)
            {
                ///Debug.Log("222");

                fireable.OnFire(getCloseEnemy());
                countdown = objectsData.GetCoolDowntime();
            }
        }
    }

}

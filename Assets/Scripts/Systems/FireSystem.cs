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
    private Audiomanager audioManager;
    private ObjectsData objectsData;
    public int audioNum;
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
        audioManager = GameObject.Find("Sfxmanager").GetComponent<Audiomanager>();
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
            if (countdown <= 0f && enemies!=null)
            {
                ///Debug.Log("222");
                audioManager.PlaySFX(audioNum);
                fireable.OnFire(getCloseEnemy());
                countdown = objectsData.GetCoolDowntime();
            }
        }
    }

}

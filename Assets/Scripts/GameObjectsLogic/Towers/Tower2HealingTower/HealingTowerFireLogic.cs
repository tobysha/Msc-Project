using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingTowerFireLogic : MonoBehaviour, IFire
{
    [SerializeField] private GameObject bullet;
    private ObjectsData Targetdata;
    private ObjectsData selfData;
    public void OnFire(Transform target)
    {
        Targetdata = target.GetComponent<ObjectsData>();
        Instantiate(bullet, target.transform.position, Quaternion.Euler(new Vector3(0, 0, 90)), target.gameObject.transform);
        Targetdata.HP += selfData.atk;
    }

    // Start is called before the first frame update
    void Start()
    {
        selfData = this.GetComponent<ObjectsData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

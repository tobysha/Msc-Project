using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherFireLogic : MonoBehaviour, IFire
{
    public GameObject bullet;
    public GameObject bulletCreate;
    private void Start()
    {
        bulletCreate = GameObject.Find("BulletCreate");
    }
    public void OnFire(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        // Quaternion rotation = Quaternion.LookRotation(direction);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // 创建旋转四元数，只绕 Z 轴旋转
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        GameObject go = Instantiate(bullet, this.transform.position, rotation, bulletCreate.gameObject.transform);
        go.GetComponent<bulletMovement>().setTarget(target);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsData : MonoBehaviour
{
    public string Name = "Monster1";
    public int HP = 100;
    public int speed = 2;
    public int atk = 3;
    [SerializeField] private GameObject atkRange;

    [SerializeField] private float CoolDowntime = 3f;
    public void ATKrange_visable(bool b)
    {
        atkRange.SetActive(b);
    }

    //fire Logic
    public float GetCoolDowntime()
    {
        return CoolDowntime;
    }
    public void ChangeCoolDownTime(float cooldown)
    {
        CoolDowntime = cooldown;
    }
}

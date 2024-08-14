using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsData : MonoBehaviour
{
    public string Name = "Monster1";
    public int HP = 100;
    public int MaxHP = 100;
    public float speed = 2f;
    public int atk = 3;
    public int value = 70;
    public int Difficulty = 100;
    [SerializeField] private GameObject atkRange;

    private float CoolDowntime;
    public void ATKrange_visable(bool b)
    {
        atkRange.SetActive(b);
    }
    private void Start()
    {
        CoolDowntime = 1 / speed;
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

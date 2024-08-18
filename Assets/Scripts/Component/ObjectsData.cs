using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectsData : MonoBehaviour
{
    public string Name = "Monster1";
    public int HP = 100;
    public int MaxHP = 100;
    public float speed = 2f;
    public int atk = 3;
    public int value = 70;
    public float Difficulty = 100;
    public int Difficulty_atk_supplement = 1;
    [SerializeField] private GameObject atkRange;

    private float CoolDowntime;

    private float Total_MaxHP = 150;
    private float Total_MaxAtk = 5.28f;
    private float Total_MaxValue = 130;
    private float Weight_HP = 0.8f;
    private float Weight_Atk = 0.4f;
    private float Weight_value = -0.2f;
    private GameManager manager;
    public void ATKrange_visable(bool b)
    {
        atkRange.SetActive(b);
    }
    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        CoolDowntime = 1 / speed;
        Difficulty_cal();
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
    private void Difficulty_cal()
    {
        Difficulty = Weight_HP * (MaxHP / Total_MaxHP) + Weight_Atk * (atk * speed * Difficulty_atk_supplement / Total_MaxAtk) + Weight_value * (value / Total_MaxValue);
        if(SceneManager.GetActiveScene().buildIndex>=4&& gameObject.CompareTag("Enemy"))
        {
            manager.Money += (int)(Difficulty * 90);
        }
        //Debug.Log(Name+": "+Difficulty);
    }
}

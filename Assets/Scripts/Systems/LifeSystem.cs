using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    public Slider HPslider;
    
    // Start is called before the first frame update
    private int MaxHP;
    private int HP;
    private int value;

    private GameManager gameData;
    void Start()
    {
        SelfDataini();
        gameData = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void SelfDataini()
    {
        ObjectsData data = this.GetComponent<ObjectsData>();
        HP = data.HP;
        MaxHP = data.MaxHP;
        value = data.value;
    }

    // Update is called once per frame
    void Update()
    {
        //HPslider.value = (float)(HP / MaxHP);
        if( HP <= 0 )
        {
            gameObject.SetActive( false );
            if(this.gameObject.CompareTag("Enemy"))
            {
                gameData.setMoney(gameData.getMoney()+value);
            }
        }
    }
    public void setHP(int i)
    {
        HP += i;
        SetHealth((float)HP, (float)MaxHP);
    }
    public void SetHealth(float health, float maxHealth)
    {
        HPslider.value = health / maxHealth;
    }
    public int getValue()
    {
        return value;
    }

}

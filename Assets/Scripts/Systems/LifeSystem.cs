using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    public Slider HPslider;
    
    // Start is called before the first frame update
    public int MaxHP = 100;
    public int HP = 100;
    public int value = 70;

    private GameManager gameData;
    void Start()
    {
        gameData = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //HPslider.value = (float)(HP / MaxHP);
        if( HP <= 0 )
        {
            Destroy( this.gameObject );
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

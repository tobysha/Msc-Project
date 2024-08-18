using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    public Slider HPslider;
    [SerializeField] private GameObject[] objectsNeedToDestroy;

    private GameManager gameData;
    ObjectsData data;
    void Start()
    {
        SelfDataini();
        gameData = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void SelfDataini()
    {
        data = this.GetComponent<ObjectsData>();
        //HP = data.HP;
        //MaxHP = data.MaxHP;
        //value = data.value;
    }

    // Update is called once per frame
    void Update()
    {
        //HPslider.value = (float)(HP / MaxHP);
        if( data.HP <= 0 )
        {
            gameObject.SetActive( false );
            if(this.gameObject.CompareTag("Enemy"))
            {
                gameData.setMoney(gameData.getMoney()+ data.value);
            }
            foreach( GameObject obj in objectsNeedToDestroy )
            {
                obj.SetActive( false );
            }
        }
        if(data.HP > data.MaxHP )
        {
            data.HP = data.MaxHP;
        }
    }
    public void setHP(int i)
    {
        data.HP += i;
        SetHealth((float)data.HP, (float)data.MaxHP);
    }
    public void SetHealth(float health, float maxHealth)
    {
        HPslider.value = health / maxHealth;
    }
    public int getValue()
    {
        return data.value;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataNeverDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameDataNeverDestroy _gameDataNeverDestroy;
    private int Money = 300;
    private float Music_Volumn;
    private float game_Volume;
    private GameObject[] enemies;
    void Start()
    {
        if (_gameDataNeverDestroy == null)
        {
            _gameDataNeverDestroy = this;
            DontDestroyOnLoad(_gameDataNeverDestroy);
        }
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(enemies);
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnemies();
    }
    public int getMoney()
    {
        return Money;
    }
    public void setMoney(int money)
    {
        Money = money;
    }
    void CheckEnemies()
    {
        // 检查所有敌人是否都被消灭
        bool allEnemiesDestroyed = true;
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                allEnemiesDestroyed = false;
                break;
            }
        }
        if (allEnemiesDestroyed)
        {
           
        }
    }
}

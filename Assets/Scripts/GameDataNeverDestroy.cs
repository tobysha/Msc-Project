using System;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*money manager:
     * mainly control money system
     */
    public int getMoney()
    {
        return Money;
    }
    public void setMoney(int money)
    {
        Money = money;
    }

}

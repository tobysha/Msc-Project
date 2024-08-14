using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataNeverDestroy : MonoBehaviour
{

    // Start is called before the first frame update
    public static GameDataNeverDestroy _gameDataNeverDestroy;

    public int[] levels = new int[10];
    public float Music_Volumn;
    public float game_Volume;
    private GameObject[] enemies;
    public int currentlevel;
    private void Awake()
    {
        Music_Volumn = 0.5f;
        game_Volume = 0.5f;
    }
    void Start()
    {
        if (_gameDataNeverDestroy == null)
        {
            _gameDataNeverDestroy = this;
            DontDestroyOnLoad(_gameDataNeverDestroy);
        }
    }

    public void ChangeCurrentLevel(int i)
    {
        currentlevel = i;
    }



}

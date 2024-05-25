using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class UIControlSystem : MonoBehaviour
{
    public GameObject tilemap;
    public GameObject tilemapComfirmButton;
    public TextMeshProUGUI moneyText;

    private RoadCreateLogic rcl;
    private GameDataNeverDestroy gdnd;
    
    public void Start()
    {
        rcl = tilemap.GetComponent<RoadCreateLogic>();
        GameObject data = GameObject.Find("GameManager");
        gdnd = data.GetComponent<GameDataNeverDestroy>();
    }
    private void Update()
    {
        moneyText.text = "Money: " + gdnd.getMoney();
    }
    public void OnComfirmRoad()
    {
        rcl.setDebugMode(false);
        rcl.SaveOriginalState();
        tilemapComfirmButton.SetActive(false);
    }
    public void OnDenyRoad()
    {
        rcl.setDebugMode(false);
        rcl.ResetTilemap();
        tilemapComfirmButton.SetActive(false);
    }
    public void OnStartGame()
    {
        rcl.setDebugMode(true);
        rcl.SaveOriginalState();
        tilemapComfirmButton.SetActive(true);
    }
    public void OnCreateTower1()
    {
        rcl.setPlacingTowerState(true);
    }
}

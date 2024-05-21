using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UIControlSystem : MonoBehaviour
{
    public GameObject tilemap;
    public GameObject tilemapComfirmButton;

    private RoadCreateLogic rcl;
    
    public void Start()
    {
        rcl = tilemap.GetComponent<RoadCreateLogic>();
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
        if (rcl.getTowerShadow() == null)
        {
            GameObject monsterShadow = Instantiate(rcl.getTowerPrefab());
            SpriteRenderer sr = monsterShadow.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                Color c = sr.color;
                c.a = 0.5f; // Semi-transparent
                sr.color = c;
            }
            rcl.setTowerShadow(monsterShadow);
        }
    }
}

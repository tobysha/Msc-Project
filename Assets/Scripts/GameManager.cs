using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameDataNeverDestroy;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class GameManager : MonoBehaviour
{
    public enum GameStage
    {
        RoadCreateStage,
        GamingStage,
        crazyStage
    }
    public float GameTime = 200;

    public int Money = 300;

    private GameStage CurrentStage = GameStage.RoadCreateStage;
    [SerializeField] private GameObject Stage1_UI;
    [SerializeField] private GameObject Stage2_UI;
    [SerializeField] private GameObject Stage3_UI;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject gameoverText;

    private GameObject[] enemies;
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(enemies);
        initialGameStage();
    }

    void Update()
    {
        //CheckEnemies();
        GameTimeCountDown();
    }

    void GameTimeCountDown()
    {
        if(CurrentStage != 0)
        {
            GameTime -= Time.deltaTime;
            if (GameTime <= 0)
            {
                gameoverText.SetActive(true);
            }
            else if (GameTime <= 30f && GameTime > 0)
            {
                CurrentStage = GameStage.crazyStage;
            }
        }
    }

 /*Game stage manager:
 For:1.Game stage chenge function 
    2.Set UI system active
 */
    public GameStage GetcurrentStage()
    {
        return CurrentStage;
    }
    public void GameStageChange(GameStage gameStage)
    {
        CurrentStage = gameStage;
        UI_StageChange();
    }
    private void initialGameStage()
    {
        Stage1_UI.SetActive(true);
        Stage2_UI.SetActive(false);
        Stage3_UI.SetActive(false);
    }
    private void UI_StageChange()
    {
        //Debug.Log((int)CurrentStage);
        switch ((int)CurrentStage)
        {
            case 0:
                Stage1_UI.SetActive(true);
                Stage2_UI.SetActive(false);
                Stage3_UI.SetActive(false);
                break;
            case 1:
                
                Stage1_UI.SetActive(true);
                Stage2_UI.SetActive(true);
                Stage3_UI.SetActive(false);
                break;
            case 2:
                Stage1_UI.SetActive(false);
                Stage2_UI.SetActive(false);
                Stage3_UI.SetActive(true);
                break;
            default:
                break;

        }
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
            winText.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public int getMoney()
    {
        return Money;
    }
    public void setMoney(int money)
    {
        Money = money;
    }
}

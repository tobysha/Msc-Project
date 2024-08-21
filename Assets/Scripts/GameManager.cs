using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameDataNeverDestroy;

public class GameManager : MonoBehaviour
{
    public enum GameStage
    {
        RoadCreateStage,
        GamingStage,
        crazyStage
    }
    public bool DebugMode = true;

    public float GameTime = 200;

    public int Money = 300;

    private GameStage CurrentStage = GameStage.RoadCreateStage;
    [SerializeField] private GameObject Stage1_UI;
    [SerializeField] private GameObject Stage2_UI;
    [SerializeField] private GameObject Stage3_UI;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject gameoverText;
    [SerializeField] private GameObject loseText;

    private Test test;
    private GameObject[] enemies;
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //Debug.Log(enemies);
        //initialGameStage();
        
    }
    private void Awake()
    {
        test = this.gameObject.GetComponent<Test>();
        iniGameMap();
        
    }

    void Update()
    {
        CheckEnemies();
        GameTimeCountDown();
    }
    public void iniGameMap()
    {
        if(SceneManager.GetActiveScene().buildIndex >= 4)// in case current scene is not toturial level or menu
        {
            if(!DebugMode)
            {
                int maxTry = 20;
                int i = 0;
                do
                {
                    if (i > maxTry)
                    {
                        break;
                    }
                    test.CreateWFC();
                    test.CreateTilemap();
                    i++;
                } while (!test.IsMapGenerateSuccess());
                    test.cleanRoads();
                test.GenerateMonsters();
                Money = test.MoneyCal;
                //Money = test.MoneyCal - GameDataNeverDestroy._gameDataNeverDestroy.currentlevel * 50;
            }
        }
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
            if(GameTime <= 0)
            {
                loseText.SetActive(true);
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
        //UI_StageChange();
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
        foreach(GameObject enemy in enemies)
        {
            if (enemy.activeSelf)
            {
                allEnemiesDestroyed = false;
                break;
            }
        }
        
        if (allEnemiesDestroyed&& !DebugMode)
        {
            winText.SetActive(true);
            GameDataNeverDestroy._gameDataNeverDestroy.levels[GameDataNeverDestroy._gameDataNeverDestroy.currentlevel] = 3;
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

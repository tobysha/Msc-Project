using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Levels_UnlockLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public Button[] buttons;
    public GameObject buttonPoint;
    private GameObject[] button_objects;
    public Sprite active_star;
    public Sprite active_level;
    //public ScriptDontDestroy leveldata;

    private void Awake()
    {
        //PlayerPrefs.SetInt("currentScore", 0);
        //PlayerPrefs.SetInt("currentLevel", 1);
    }
    void Start()
    {
        //int unlock = PlayerPrefs.GetInt("currentLevel");
        //Debug.Log(unlock);
        //button_objects = new GameObject[buttonPoint.transform.childCount];
        buttons = new Button[buttonPoint.transform.childCount];
        for (int i = 0; i < buttonPoint.transform.childCount; i++)
        {
            buttons[i] = buttonPoint.transform.GetChild(i).GetComponent<Button>();
        }
        for (int i = 1; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; GameDataNeverDestroy._gameDataNeverDestroy.levels[i] > 0; i++)
        {
            buttons[i].interactable = true;
            buttons[i].GetComponent<Image>().sprite = active_level;
            unlock_level(i);
            if (i + 1 < GameDataNeverDestroy._gameDataNeverDestroy.levels.Length && GameDataNeverDestroy._gameDataNeverDestroy.levels[i]==3 ) //unlock the newest level
            {
                if(GameDataNeverDestroy._gameDataNeverDestroy.levels[i + 1] == 0)
                {
                    buttons[i+1].interactable = true;
                    buttons[i+1].GetComponent<Image>().sprite = active_level;
                    unlock_level(i+1);
                }
            }
            getState(i);
        }
        
        

    }
    void unlock_level(int num)
    {
        GameObject[] objects = new GameObject[4];
        //GameObject[] objects = buttons[num].GetComponentsInChildren<GameObject>(false);
        for (int i = 0; i < 4; i++)
        {
            objects[i] = buttons[num].transform.GetChild(i).gameObject;

        }
        //objects[0] = buttons[num].transform.GetChild(0).gameObject;
        //objects[1] = buttons[num].transform.Find("star_1").gameObject;
        //objects[2] = buttons[num].transform.Find("star_2").gameObject;
        //objects[3] = buttons[num].transform.Find("star_3").gameObject;
        foreach (var star in objects)
        {
            star.SetActive(true);
        }
    }
    void getState(int num)
    {
        if(num < 0)
        {
            return;
        }
        //active gameobjects

        //active the stars
        //int score_of_star = PlayerPrefs.GetInt("currentScore");
        //Debug.Log(score_of_star);
        int score_of_star = GameDataNeverDestroy._gameDataNeverDestroy.levels[num];
        Image[] stars = buttons[num].GetComponentsInChildren<Image>();
        //int score_of_star = DataController.level_score[0];
        for (int i = 1; score_of_star > 0; score_of_star--,i++)
        {
            stars[i].sprite = active_star;
        }
    }
    void full_star(int num)
    {
        Image[] stars = buttons[num].GetComponentsInChildren<Image>();
        //int score_of_star = DataController.level_score[0];
        for (int i = 1; i<4;  i++)
        {
            stars[i].sprite = active_star;
        }
    }
    
    
}

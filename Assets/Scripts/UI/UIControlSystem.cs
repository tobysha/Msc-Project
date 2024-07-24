using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class UIControlSystem : MonoBehaviour
{
    [SerializeField] private GameObject tilemap;
    [SerializeField] private GameObject tilemapComfirmButton;
    [SerializeField] private TextMeshProUGUI moneyText;

    private RoadCreateLogic rcl;
    private GameManager gameManager;

    private int CurGameScene;

    public GameObject levelselection;
    public GameObject settingmenu;
    private GameObject currentButton;
    public GameObject changing_state_UI;
    public GameObject StartMenu;
    public GameObject PauseMenu;
    ///////music part/////////
    private AudioSource musicAudio;
    public AudioClip buttonClip;
    public AudioSource gameAudio;
    public Slider Music_slider;
    public Slider game_sound_slider;
    public void Start()
    {
        CurGameScene = SceneManager.GetActiveScene().buildIndex;
        if(CurGameScene!=0)//防止在manu运行下面的东西
        {
            rcl = tilemap.GetComponent<RoadCreateLogic>();
            GameObject data = GameObject.Find("GameManager");
            gameManager = data.GetComponent<GameManager>();
        }
    }
    /*-----------------Menu UI---------------------------*/
    public void On_start_button()
    {
        StartMenu.SetActive(false);
        levelselection.SetActive(true);
        gameAudio.PlayOneShot(buttonClip);

    }
    public void On_setting_button()
    {
        StartMenu.SetActive(false);
        settingmenu.SetActive(true);
        gameAudio.PlayOneShot(buttonClip);
    }
    public void On_selection_button(int j)
    {
        /*currentButton = EventSystem.current.currentSelectedGameObject;
        string s = currentButton.GetComponent<TextMeshPro>().text;
        int i = 0;
        int.TryParse(s, out i);//Change the value tpye into int*/
        gameAudio.PlayOneShot(buttonClip);
        try
        {
            SceneManager.LoadScene(j);
        }
        catch
        {
            Debug.Log("No such level select");
        }
    }
    public void On_levelExit_button()
    {
        levelselection.SetActive(false);
        StartMenu.SetActive(true);
        gameAudio.PlayOneShot(buttonClip);

    }
    public void On_settingExit_button()
    {
        settingmenu.SetActive(false);
        StartMenu.SetActive(true);
        gameAudio.PlayOneShot(buttonClip);
    }
    public void OnExitGame()
    {
        gameAudio.PlayOneShot(buttonClip);
#if UNITY_EDITOR
        //UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    /*Stage1 UI*/
    private void Update()
    {
        moneyText.text = "Money: " + gameManager.getMoney();
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
    public void OnFinishButton()
    {
        gameManager.GameStageChange(GameManager.GameStage.GamingStage);
    }


    /*Stage2 UI*/
    public void OnCreateTower1()
    {
        rcl.setPlacingTowerState(true);
    }
}

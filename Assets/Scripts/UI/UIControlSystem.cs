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

    [SerializeField] private GameObject[] ToturialTexts;
    int currentText = 0;
    public void Start()
    {
        CurGameScene = SceneManager.GetActiveScene().buildIndex;
        if(CurGameScene!=0)//防止在manu运行下面的东西
        {
            rcl = tilemap.GetComponent<RoadCreateLogic>();
            GameObject data = GameObject.Find("GameManager");
            gameManager = data.GetComponent<GameManager>();
        }
        music_volume_setting();
    }
    void music_volume_setting()
    {
        musicAudio = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (GameDataNeverDestroy._gameDataNeverDestroy != null)
            {
                musicAudio.volume = GameDataNeverDestroy._gameDataNeverDestroy.Music_Volumn;
                gameAudio.volume = GameDataNeverDestroy._gameDataNeverDestroy.game_Volume;
            }
        }
        else
        {
            if (GameDataNeverDestroy._gameDataNeverDestroy != null)
            {
                Music_slider.value = GameDataNeverDestroy._gameDataNeverDestroy.Music_Volumn;
                game_sound_slider.value = GameDataNeverDestroy._gameDataNeverDestroy.game_Volume;
            }
        }
    }
    /*-----------------Menu UI---------------------------*/

    public void On_Music_Value_Change(Slider slider)
    {
        //Debug.Log(slider.value);
        musicAudio.volume = slider.value;
        GameDataNeverDestroy._gameDataNeverDestroy.Music_Volumn = slider.value;
    }
    public void On_Game_Value_Change(Slider slider)
    {
        gameAudio.volume = slider.value;
        GameDataNeverDestroy._gameDataNeverDestroy.game_Volume = slider.value;
    }
    public void On_start_button()
    {
        StartMenu.SetActive(false);
        levelselection.SetActive(true);
        //gameAudio.PlayOneShot(buttonClip);

    }
    public void On_setting_button()
    {
        StartMenu.SetActive(false);
        settingmenu.SetActive(true);
        //gameAudio.PlayOneShot(buttonClip);
    }
    public void On_selection_button(int j)
    {
        /*currentButton = EventSystem.current.currentSelectedGameObject;
        string s = currentButton.GetComponent<TextMeshPro>().text;
        int i = 0;
        int.TryParse(s, out i);//Change the value tpye into int*/
        //gameAudio.PlayOneShot(buttonClip);
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
        //gameAudio.PlayOneShot(buttonClip);

    }
    public void On_settingExit_button()
    {
        settingmenu.SetActive(false);
        StartMenu.SetActive(true);
        //gameAudio.PlayOneShot(buttonClip);
    }
    public void OnExitGame()
    {
        //gameAudio.PlayOneShot(buttonClip);
#if UNITY_EDITOR
        //UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    /*----------------------------------Gaming UI-----------------*/
    public void On_pause_button()
    {
        //gameAudio.PlayOneShot(buttonClip);
        PauseMenu.SetActive(true);
        Score_check();
        Time.timeScale = 0;

    }
    public void On_Resume_button()
    {
        //gameAudio.PlayOneShot(buttonClip);
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void On_Restart_button()
    {
        //gameAudio.PlayOneShot(buttonClip);
        Time.timeScale = 1;
        int currentscene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentscene);
    }
    public void On_Menu_button()
    {
        //gameAudio.PlayOneShot(buttonClip);
        Time.timeScale = 1;
        Score_check();
        SceneManager.LoadScene(0);
    }
    public void On_Next_level()
    {
        //gameAudio.PlayOneShot(buttonClip);
        Time.timeScale = 1;
        Score_check();
        int currentscene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentscene + 1);
    }
    private void Score_check()
    {

        int score = 0;
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (score > GameDataNeverDestroy._gameDataNeverDestroy.levels[currentLevel - 1])
        {
            GameDataNeverDestroy._gameDataNeverDestroy.levels[currentLevel - 1] = score;

        }
        //PlayerPrefs.SetInt("currentScore", i);
    }

    /*-----------------------------------------tutorial Text-------------------------------*/
    public void Tutorial1_Next_page()
    {
        if(ToturialTexts!=null)
        {
            if(currentText >= 0)
            {
                ToturialTexts[currentText].SetActive(false);
            }
            if(currentText+1 < ToturialTexts.Length)
            {
                currentText++;
                ToturialTexts[currentText].SetActive(true);
            }
        }
    }
    public void Tutorial_camera_text(GameObject go)
    {
        go.SetActive(false);
    }



    /*Stage1 UI*/
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            moneyText.text =  gameManager.getMoney().ToString();
        }
    }
    public void OnComfirmRoad()
    {
        Time.timeScale = 1f;
        rcl.setDebugMode(false);
        rcl.SaveOriginalState();
        tilemapComfirmButton.SetActive(false);
    }
    public void OnDenyRoad()
    {
        Time.timeScale = 1f;
        rcl.setDebugMode(false);
        rcl.ResetTilemap();
        tilemapComfirmButton.SetActive(false);
    }
    public void OnStartGame()
    {
        Time.timeScale = 0.1f;
        rcl.setDebugMode(true);
        rcl.SaveOriginalState();
        tilemapComfirmButton.SetActive(true);
    }
    public void OnFinishButton()
    {
        Time.timeScale = 1f;
        rcl.setDebugMode(false);
        rcl.SaveOriginalState();
        gameManager.GameStageChange(GameManager.GameStage.GamingStage);
        Time.timeScale = 0;
    }


    /*Stage2 UI*/
    public void OnCreateTower(GameObject gameObject)
    {
        rcl.setPlacingTowerState(true, gameObject);
    }

    
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GametimeCountDown : MonoBehaviour
{
    private TextMeshProUGUI timeText;
    private GameManager gameManager;
    private void Start()
    {
        timeText = this.gameObject.GetComponent<TextMeshProUGUI>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        TimeToMin();
    }
    void TimeToMin()
    {
        if(gameManager.GameTime > 0)
        {
            float totaltime = gameManager.GameTime;
            int min = (int)totaltime / 60;
            int seconds = (int)totaltime % 60;
            //Debug.Log(seconds);
            timeText.text = "Time: " + min.ToString() + ":" + seconds.ToString();
        }
        else
        {
            timeText.text = "Time:0:00";
        }
        
    }
}

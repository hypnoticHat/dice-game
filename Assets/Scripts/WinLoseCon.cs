using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WinLoseCon : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public GameObject winCondition;
    public GameObject loseCondition;
    public GameObject restartBtn;
    public GameObject scriptHolder;
    public GameObject dice;
    public int numScore;

    void Update()
    {
        
        if(Convert.ToInt32(scoreText.text) >= 20 && timeText.text == "00:0"){
            winCondition.SetActive(true);
            restartBtn.SetActive(true);
            timeText.text = "Time Over";
            dice.SetActive(false);
            scriptHolder.GetComponent<TimerCountDown>().enabled = false;
            scriptHolder.SetActive(false);
        }else if( Convert.ToInt32(scoreText.text) < 20 && timeText.text == "00:0"){
            loseCondition.SetActive(true);
            restartBtn.SetActive(true);
            timeText.text = "Time Over";
            dice.SetActive(false);
            scriptHolder.GetComponent<TimerCountDown>().enabled = false;
            scriptHolder.SetActive(false);
        }else if(restartBtn.activeInHierarchy == false){
            winCondition.SetActive(false);
            loseCondition.SetActive(false);
            scriptHolder.GetComponent<TimerCountDown>().enabled = true;
        }
    }
}

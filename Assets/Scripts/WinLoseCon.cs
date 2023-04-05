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

    // void Start()
    // {

    // }

    void Update()
    {
        if( Convert.ToInt32(scoreText) >= 20 && timeText.text == "00:0"){
            winCondition.SetActive(true);
            restartBtn.SetActive(true);
            timeText.text = "Time Over";
            dice.SetActive(true);
            scriptHolder.GetComponent<TimerCountDown>().enabled = false;
            // scriptHolder.SetActive(false);
        }else if( Convert.ToInt32(scoreText) < 20 && timeText.text == "00:0"){
            loseCondition.SetActive(true);
            restartBtn.SetActive(true);
            timeText.text = "Time Over";
            scriptHolder.GetComponent<TimerCountDown>().enabled = false;
            dice.SetActive(false);
            // scriptHolder.SetActive(false);
        }else if(restartBtn.activeInHierarchy == false){
            winCondition.SetActive(false);
            loseCondition.SetActive(false);
            dice.SetActive(true);
            // scriptHolder.GetComponent<TimerCountDown>().enabled = true;
        }
    }

    // public void WinOrLose () {
    //     if( Convert.ToInt32(scoreText) >= 20 && timeText.text == "00:0"){
    //         winCondition.SetActive(true);
    //         restartBtn.SetActive(true);
    //         timeText.text == "Time"
    //     }else if( Convert.ToInt32(scoreText) < 20 && timeText.text == "00:0"){
    //         loseCondition.SetActive(true);
    //         restartBtn.SetActive(true);
    //         timeText.text == "Time"
    //     }else if(restartBtn == null){
    //         winCondition.SetActive(false);
    //         loseCondition.SetActive(false);
    //     }
    // }

    // public void WinCon(){
    //     if( Convert.ToInt32(scoreText) >= 20 && timeText.text == "00:0"){
    //         winCondition.SetActive(true);
    //         restartBtn.SetActive(true);
    //     }
    // } 
    // public void LoseCon(){
    //     if( Convert.ToInt32(scoreText) < 20 && timeText.text == "00:0"){
    //         loseCondition.SetActive(true);
    //         restartBtn.SetActive(true);
    //     }
    // } 
    // public void Restart(){
    //     if(restartBtn == null){
    //         winCondition.SetActive(false);
    //         loseCondition.SetActive(false);
    //     }
    // }
}

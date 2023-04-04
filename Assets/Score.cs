using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    int myScore = 1;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = myScore.ToString();
    }
    public void AddScore(int score){
        myScore += score;
    }
}

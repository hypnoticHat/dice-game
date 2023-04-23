using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswScript : MonoBehaviour
{
    // public TextMeshProUGUI scoreUI;
    // int ansPoint = 5;
    public ScoreManager scoreManager;

    public bool isCorrect = false;
    public QuizManager quizManager;
    // public NewScore newScore;
    public GameObject correctUI;
    public GameObject inCorrectUI;

    public SoundEffect soundEffect;
    public void Answer()
    {
        //add score if right and pepare another question
        if (isCorrect)
        {
            StartCoroutine(CorrectAns());

           
        }
        // prepare another question and turn off question screen
        else
        {
            StartCoroutine(inCorrect());

        }
    }


    IEnumerator CorrectAns()
    {
        //play audio
        soundEffect.src.clip = soundEffect.correctAudio;
        soundEffect.src.Play();

        //show right ui
        correctUI.SetActive(true);
        yield return new WaitForSeconds(1);
        correctUI.SetActive(false);
        // scoreUI.text += ansPoint.ToString();
        // newScore.score += 5;
        scoreManager.score += 5;
        print("++5");
        quizManager.correct();

    }

    IEnumerator inCorrect()
    {
        //play audio
        soundEffect.src.clip = soundEffect.inCorrectAudio;
        soundEffect.src.Play();

        //show wrong ui
        inCorrectUI.SetActive(true);
        yield return new WaitForSeconds(1);
        inCorrectUI.SetActive(false);
        quizManager.correct();


    }

}

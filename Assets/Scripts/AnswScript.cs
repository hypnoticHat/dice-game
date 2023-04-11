using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;
    public NewScore newScore;
    public GameObject correctUI;
    public GameObject inCorrectUI;
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
        correctUI.SetActive(true);

        yield return new WaitForSeconds(1);
        correctUI.SetActive(false);
        newScore.score += 10;
        quizManager.correct();
    }

    IEnumerator inCorrect()
    {
        inCorrectUI.SetActive(true);
        yield return new WaitForSeconds(1);
        inCorrectUI.SetActive(false);
        quizManager.correct();

    }

}

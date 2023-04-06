using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;
    public NewScore newScore;
    public void Answer()
    {
        //add score if right and pepare another question
        if (isCorrect)
        {
            newScore.score += 10;
            quizManager.correct();
        }
        // prepare another question and turn off question screen
        else
        {
            quizManager.correct();
        }
    }
}

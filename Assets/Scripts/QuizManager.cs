using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnsw> QnA;
    public GameObject[] options;
    public int currentQuestion;
    public GameObject QuestionUI;
    public GameObject Dice;

    public TMP_Text text;
    public TimerCountDown time;

    private void Start()
    {
        GenerateQuestion();

    }

    public void correct()
    {
        //remove the Question Generated out of system (open when needed)
        //QnA.RemoveAt(currentQuestion);

        //create a new question
        QuestionUI.SetActive(false);
        //allow using dice
        Dice.GetComponent<DiceRoll>().enabled = true;
        Dice.SetActive(true);
        GenerateQuestion();
        time.stop = false;
    }

    //create a loop to set all answer 
    void setAnswer()
    {
        for(int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[currentQuestion].Answers[i];

            if(QnA[currentQuestion].CorrectAnswer == i)
            {
                options[i].GetComponent<AnswScript>().isCorrect = true;
            }
        }
    }

    //random taking question
    void GenerateQuestion()
    {
        currentQuestion = Random.Range(0, QnA.Count);

        text.text = QnA[currentQuestion].Question;
        setAnswer();

    }
}

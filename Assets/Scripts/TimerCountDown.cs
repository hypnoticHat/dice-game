using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class TimerCountDown : MonoBehaviour
{
    //time slider
    public Slider slider;

    //time text
    public TextMeshProUGUI timeText;
    public GameObject dice;
    public int secondsLeft = 45;
    public bool takingAway = true;
    public bool stop;

    // Start is called before the first frame update
    void Start()
    {
        timeText.text = "00:" + secondsLeft.ToString();
        slider.maxValue = secondsLeft;//make slider max values = max time
    }

    // Update is called once per frame
    void Update()
    {

        if (takingAway == false && secondsLeft > 0 && !stop){
            StartCoroutine(TimerTake());
        } else if (secondsLeft == 0){
            secondsLeft = 45;
            dice.GetComponent<DiceRoll>().enabled = false;
        }
        else
        {
            return;
        }
        
    }
    
    IEnumerator TimerTake(){
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        slider.value = secondsLeft; //uodate to slider
        timeText.text = "00:" + secondsLeft.ToString();
        takingAway = false;

        //make dice onlly work when time count down
        dice.GetComponent<DiceRoll>().enabled = true;

    }

    //set bool to button start (fixed the time start countdown at very start)
    public void setBool()
    {
        takingAway = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerCountDown : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public int secondsLeft = 45;
    public bool takingAway = false;
    // Start is called before the first frame update
    void Start()
    {
        timeText.text = "00:" + secondsLeft.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (takingAway == false && secondsLeft > 0){
            StartCoroutine(TimerTake());
        }
    }
    
    IEnumerator TimerTake(){
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        timeText.text = "00:" + secondsLeft.ToString();
        takingAway = false;
    }

    

}

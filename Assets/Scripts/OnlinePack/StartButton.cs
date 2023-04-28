using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class StartButton : NetworkBehaviour
{
    public GameObject StartBG;
    public TimerCountDown timer;
    public DiceRoll diceRoll;
    public GameObject TimeSlide;
    public GameObject chanceUI;
    public GameObject startBtn;

    public void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClickClientRpc);
    }

    [ClientRpc]
    public void OnClickClientRpc()
    {
        timer.setBool();
        Debug.Log("clickced");
        StartBG.SetActive(false);
        diceRoll.enabled = true;
        TimeSlide.SetActive(true);
        chanceUI.SetActive(false);
        startBtn.SetActive(false);
    }
}

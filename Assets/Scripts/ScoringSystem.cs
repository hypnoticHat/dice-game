using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoringSystem : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int theScore;
    // public AudioSource collectSound;

    void OnTriggerEnter(Collider other)
    {
        // collectSound.Play();
        theScore += Convert.ToInt32(scoreText.text) + 1;
        scoreText.text = theScore.ToString();
        Destroy(gameObject);
        UnityEngine.Debug.Log("Ok");
    }
}

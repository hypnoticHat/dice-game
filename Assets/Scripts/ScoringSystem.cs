using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoringSystem : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int theScore;
    // public AudioSource collectSound;

    void OnTriggerEnter(Collider other)
    {
        // collectSound.Play();
        theScore += 1;
        scoreText.text = theScore.ToString();
        Destroy(gameObject);
        UnityEngine.Debug.Log("Ok");
    }
}

using UnityEngine;
using TMPro;

public class NewScore : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreUI;

    public AudioSource src;
    public AudioClip collectCoinSound;
    //string tag = "Point"; // your tag why this here
    GameObject[] taggedObjects;
    public void Start() {
        taggedObjects = GameObject.FindGameObjectsWithTag("Point");
    }


    void Update()
    {
        scoreUI.text = score.ToString();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Point"){
            //play sound
            src.clip = collectCoinSound;
            src.Play();
            //add score
            score ++;
            other.gameObject.SetActive(false);
        }else if (other.gameObject.tag == "Start"){
            foreach (GameObject tagged in taggedObjects){
                tagged.SetActive(true); // or true
            }
        }
    }

}

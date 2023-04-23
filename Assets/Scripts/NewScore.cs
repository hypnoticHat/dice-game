using UnityEngine;
using TMPro;
using Unity.Netcode;

public class NewScore : NetworkBehaviour
{
    public int score;
    public TextMeshProUGUI scoreUI;

    public AudioSource src;
    public AudioClip collectCoinSound;
    ScoreManager scoreManager;
    //string tag = "Point"; // your tag why this here
    GameObject[] taggedObjects;
    public void Start() {
        scoreUI = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        src = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        scoreManager = GameObject.Find("ScoreText").GetComponent<ScoreManager>();

        taggedObjects = GameObject.FindGameObjectsWithTag("Point");
    }


    void Update()
    {
        if(!IsOwner) return;
        // score = scoreUI.int;
        // scoreUI.text = score.ToString();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Point"){
            //play sound
            src.clip = collectCoinSound;
            src.Play();
            //add score
            scoreManager.score ++;
            // score ++;       
            other.gameObject.SetActive(false);
        }else if (other.gameObject.tag == "Start"){
            foreach (GameObject tagged in taggedObjects){
                tagged.SetActive(true); // or true
            }
        }
    }

}

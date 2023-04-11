using UnityEngine;
using TMPro;

public class NewScore : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreUI;

    string tag = "Point"; // your tag
    GameObject[] taggedObjects;
    public void Start() {
        taggedObjects = GameObject.FindGameObjectsWithTag(tag);
    }


    void Update()
    {
        scoreUI.text = score.ToString();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Point"){
            score ++;
            other.gameObject.SetActive(false);
        }else if (other.gameObject.tag == "Start"){
            foreach (GameObject tagged in taggedObjects){
                tagged.SetActive(true); // or true
            }
        }
    }

}

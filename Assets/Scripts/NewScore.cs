using UnityEngine;
using TMPro;

public class NewScore : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreUI;

    // Update is called once per frame
    void Update()
    {
        scoreUI.text = score.ToString();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Point"){
            score ++;
            Destroy(other.gameObject);
        }
    }
}

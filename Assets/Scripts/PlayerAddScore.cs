using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAddScore : MonoBehaviour
{
    public Score score; 



    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Score")) 
        {
            score.AddScore(1);
            Debug.Log("+");
        }       
    } 
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Score")) 
        {
            score.AddScore(1);
            Debug.Log("+");
        }       
    } 
}

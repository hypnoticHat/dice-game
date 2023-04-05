using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoverment : MonoBehaviour
{
    // GameObject Player = GameObject.Find("Player");
    // GameObject Cylinder = Player.transform.GetChild(0).gameObject;
    // GetComponent<Collider>().enabled = false; 
    public GameObject point;

    public Route CurrentRoute;
    int routePosition;
    public int steps;
    bool isMoving;
    string currentNode;

    //geting dice values
    public GameObject Dice;
    DiceRoll DiceRoll;

    private void Start()
    {
        DiceRoll = Dice.GetComponent<DiceRoll>();
    }

    // void Start()
    // {
    //     Collider collider = ColliderTransform.GetChild(0).GetComponent<Collider>();
    // }

    private void Update()
    {
        //geting values from dice and start moving base on those values
        if(DiceRoll.GetedValue && !isMoving)
        {
            steps = DiceRoll.diceValue;
            Debug.Log("run "+steps);
            StartCoroutine(Move());
            DiceRoll.GetedValue = false;
            // score.AddScore(1);


            if (point != null){
                point.GetComponent<Collider>().enabled = false; 
            }
            
            //not for loot route
            /*if (routePosition + steps < CurrentRoute.childNodeList.Count)
            {
                StartCoroutine(Move());
            }
            else
            {
                Debug.Log("Roll number is to high");
            }*/
  
        }
        if (point != null && !isMoving){
            point.GetComponent<Collider>().enabled = true; 
        } 
        
    }

    //create movement for frame
    IEnumerator Move()
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        while(steps > 0)
        {
            routePosition++;
            routePosition %= CurrentRoute.childNodeList.Count;
            Vector3 nextPos = CurrentRoute.childNodeList[routePosition].position;
            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }
                
            yield return new WaitForSeconds(0.1f);
            steps--;
        }
        currentNode = CurrentRoute.childNodeList[routePosition].ToString();
        isMoving = false;
        //return current node name
        specialNoded(currentNode.Split(" ")[0]);
    }

    //move to the position base on dice
    bool MoveToNextNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 6f * Time.deltaTime));
    }

    //special node 
    void specialNoded(string currenNode) 
    {
        //telephot if player hit this node * need to fix *
        if (currenNode == "Telephot")
        {
            routePosition = 12;
        }
    }
}

    // void OnTriggerEnter(Collider other) {
    //     if (other.gameObject.CompareTag("Point")) 
    //     {
    //         score.AddScore(1);
    //         Debug.Log("+");
    //     }       
    // } 
    // private void OnCollisionEnter(Collision other) {
    //     if (other.gameObject.layer == LayerMask.NameToLayer("Point")) 
    //     {
    //         score.AddScore(1);
    //         Debug.Log("+");
    //     }       
    // } 



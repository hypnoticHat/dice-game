using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoverment : MonoBehaviour
{
    public GameObject cylinderCollision;

    public int EndTelephot;
    public Route CurrentRoute;
    int routePosition;
    public int steps;
    bool isMoving;
    string currentNode;
    public GameObject ChanceUI;
    public NewScore NewScore;
    public GameManager GameManager;

    //geting dice values
    public GameObject Dice;
    public TimerCountDown TimerCountDown;

    DiceRoll DiceRoll;
    

    private void Start()
    {
        DiceRoll = Dice.GetComponent<DiceRoll>();
        //stop dice roll from start of the gamme
        Dice.GetComponent<DiceRoll>().enabled = false;

    }


    private void Update()
    {
        //geting values from dice and start moving base on those values
        if(DiceRoll.GetedValue && !isMoving)
        {
            
            steps = DiceRoll.diceValue;
            StartCoroutine(Move());
            DiceRoll.GetedValue = false;

            isMoving = true;
            print("False");
            Dice.GetComponent<DiceRoll>().enabled = false;


            cylinderCollision.GetComponent<Collider>().enabled = false;
            
  
        }else if (isMoving==true){

            cylinderCollision.GetComponent<Collider>().enabled = true;
            Dice.GetComponent<DiceRoll>().enabled = false;
            Dice.SetActive(false);
        }else if (isMoving == false){
            Dice.GetComponent<DiceRoll>().enabled = true;
            Dice.SetActive(true);
        }
        
        
    }

    //create movement for frame
    IEnumerator Move()
    {
        if (isMoving)
        {
            yield break;
        }
        // isMoving = true;

        //zoom camera in
        //GameManager.zoomCamIn();

        //move player 
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
            //give player 10 score if go thru finish line
            if (routePosition == 0)
            {
                NewScore.score += 10;
            }
        }
        //make camera zoom out
        //GameManager.zoomCamOut();
        //return current node name
        currentNode = CurrentRoute.childNodeList[routePosition].ToString();
        isMoving = false;
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
        switch (currenNode)
        {
            //telephot player
            case "Telephot":
                routePosition += EndTelephot;
                break;
            //question
            case "chance":
                ChanceUI.SetActive(true);
                //stop using dice
                TimerCountDown.stop = true;
                Dice.SetActive(false);
                break;
        }
    }
}



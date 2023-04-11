using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoverment : MonoBehaviour
{
    public GameObject cylinderCollision;

    public int EndTelephot;
    public Route CurrentRoute;
    public int steps;
    public GameObject ChanceUI;
    public NewScore NewScore;
    public GameManager GameManager;
    public SoundEffect soundEffect;

    int routePosition;
    bool isMoving;
    string currentNode;

    //geting dice values
    public GameObject Dice;
    public TimerCountDown TimerCountDown;
    DiceRoll DiceRoll;
    

    private void Start()
    {
        DiceRoll = Dice.GetComponent<DiceRoll>();
        //stop dice roll from start of the gamme
        DiceRoll.enabled = false;

    }


    private void Update()
    {
        //geting values from dice and start moving base on those values
        if(DiceRoll.GetedValue && !isMoving)
        {
            
            steps = DiceRoll.diceValue;
            StartCoroutine(Move());
            DiceRoll.GetedValue = false;

            
  
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

        //zoom camera in
        //GameManager.zoomCamIn();

        //move player 
        while(steps > 0)
        {
            //stop roll dice
            DiceRoll.enabled = false;
            //cylinderCollision.GetComponent<Collider>().enabled = true;

            //add new postition to move
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
                soundEffect.src.clip = soundEffect.passFinishSound;
                soundEffect.src.Play();
                NewScore.score += 10;
            }
        }

        //make camera zoom out
        //GameManager.zoomCamOut();
        //return current node name
        currentNode = CurrentRoute.childNodeList[routePosition].ToString();
        isMoving = false;
        specialNoded(currentNode.Split(" ")[0]);

        //alow roll again
        DiceRoll.enabled = true;
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
                soundEffect.src.clip = soundEffect.hitJumpNode;
                soundEffect.src.Play();

                routePosition += EndTelephot;
                break;
            //question
            case "chance":
                ChanceUI.SetActive(true);
                soundEffect.src.clip = soundEffect.hitChanceNode;
                soundEffect.src.Play();
                //stop using dice
                TimerCountDown.stop = true;
                Dice.SetActive(false);
                break;
            //go back 3 node
            case "BackNode":
                routePosition -= 4;
                steps = 1;
                StartCoroutine(Move());
                break;
            

        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoverment : MonoBehaviour
{
    public Route CurrentRoute;
    int routePosition;
    public int steps;
    bool isMoving;
    string currentNode;
    int score;
    public GameObject ChanceUI;

    //geting dice values
    public GameObject Dice;
    DiceRoll DiceRoll;

    private void Start()
    {
        DiceRoll = Dice.GetComponent<DiceRoll>();
    }
    private void Update()
    {
        //geting values from dice and start moving base on those values
        if(DiceRoll.GetedValue && !isMoving)
        {
            steps = DiceRoll.diceValue;
            Debug.Log("run "+steps);
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
        switch (currenNode)
        {
            case "Telephot":
                routePosition = 12;
                break;
            case "chance":
                ChanceUI.SetActive(true);
                break;
        }
    }

    public void rightAns()
    {
        score++;
        ChanceUI.SetActive(false);
        Debug.Log(score);
    }

    public void wrongAns()
    {
        ChanceUI.SetActive(false);
        return;
    }
}
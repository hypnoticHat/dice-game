using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    //detect and create node
    Transform[] childObject;
    public List<Transform> childNodeList = new List<Transform>();
    public GameObject point;

    //materiall
    public Material[] materials;
    //draw a line conect each child node
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        FillNodes();

        for (int i = 0; i < childNodeList.Count; i++)
        {
            Vector3 CurrentPos = childNodeList[i].position;
            if(i > 0)
            {
                Vector3 prevPos = childNodeList[i - 1].position;
                Gizmos.DrawLine(prevPos,CurrentPos);
            }
            
        }
        SpawnMaterial();
    }

    private void Start()
    {
        SpawnPoint();

    }

    //auto find and fill child node into Route
    void FillNodes()
    {
        childNodeList.Clear();

        childObject = GetComponentsInChildren<Transform>();

        foreach(Transform child in childObject)
        {
            if(child != transform)
            {
                childNodeList.Add(child);
            }
        }
    }

    //create point in map for each node created
    public void SpawnPoint()
    {
        for (int i = 0; i < childNodeList.Count; i++)
        {
            //create point right node position + 0.5 in Y
            if (i % 4 == 0 && i != 0)
            {
                Instantiate(point, childNodeList[i].position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            }
        }
    }
    public void SpawnMaterial()
    {
        for (int i = 0; i < childNodeList.Count; i++)
        {
            string currentNode = childNodeList[i].ToString();

            GameObject child = transform.GetChild(i).gameObject;

            switch (currentNode.Split(" ")[0])
            {
                //telephot player
                case "Telephot":
                    child.GetComponent<Renderer>().material = materials[0];
                    break;
                //question
                case "chance":
                    child.GetComponent<Renderer>().material = materials[1];
                    break;
                //go back 3 node
                case "BackNode":
                    child.GetComponent<Renderer>().material = materials[2];
                    break;
                //shake challen
                case "StartLine":
                    child.GetComponent<Renderer>().material = materials[3];
                    break;
                case "EndTelephot":
                    child.GetComponent<Renderer>().material = materials[4];
                    break;
                case "Forward":
                    child.GetComponent<Renderer>().material = materials[5];
                    break;
                case "TimeUp":
                    child.GetComponent<Renderer>().material = materials[7];
                    break;
                default:
                    child.GetComponent<Renderer>().material = materials[6];
                    break;
            }
        }
    }
    }

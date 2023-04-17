using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    //detect and create node
    Transform[] childObject;
    public List<Transform> childNodeList = new List<Transform>();
    public GameObject point;

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
        for (int i = 1; i < childNodeList.Count; i++)
        {
            //create point right node position + 0.5 in Y
            if (i % 4 == 0)
            {
               Instantiate(point, childNodeList[i].position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            }
        }
    }
}

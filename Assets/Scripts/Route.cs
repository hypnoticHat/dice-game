using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    //detect and create node
    Transform[] childObject;
    public List<Transform> childNodeList = new List<Transform>();

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    Transform[] childObject;
    public List<Transform> childNodeList = new List<Transform>();

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
    bool onGround;
    public int sideValue;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Ground")
        {
            onGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Ground")
        {
            onGround=false;
        }
    }

    public bool OnGround()
    {
        return onGround;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSide : MonoBehaviour
{

    bool onGround;
    public int sideValue;

    //if hit ground
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Ground")
        {
            onGround = true;
        }
    }

    //if it bound out of ground
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Ground")
        {
            onGround=false;
        }
    }

    //return the stagement of the dice
    public bool OnGround()
    {
        return onGround;
    }
}

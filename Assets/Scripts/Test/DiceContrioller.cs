using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceContrioller : MonoBehaviour
{
    public float ShakeForceMultiplier;
    public Rigidbody[] shakingRigidbody;

    //shake the dice up
    //using for testing so you dont need to focus on this
    public void ShakeRigidbodies(Vector3 deviceAcceleration)
    {
        foreach (var rigidbody in shakingRigidbody)
        {
            rigidbody.AddForce(deviceAcceleration * ShakeForceMultiplier, ForceMode.Impulse);
        }
    }

}

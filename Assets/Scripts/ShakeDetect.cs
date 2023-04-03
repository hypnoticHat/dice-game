using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [RequireComponent(typeof(DiceContrioller))]
public class ShakeDetect : MonoBehaviour
{
    public float shakeDetectionThreshold;
    public float minShakeInterval;

    private float sqrShakeDetectionthreshold;
    private float timeSinceLastShake;

    private DiceContrioller DiceContrioller;

    private void Start()
    {
        sqrShakeDetectionthreshold = Mathf.Pow(shakeDetectionThreshold,2 );
        DiceContrioller = GetComponent<DiceContrioller>();
    }

    private void Update()
    {
        if(Input.acceleration.sqrMagnitude >= sqrShakeDetectionthreshold && Time.unscaledTime >= timeSinceLastShake + minShakeInterval)
        {
            DiceContrioller.ShakeRigidbodies(Input.acceleration);
            timeSinceLastShake = Time.unscaledTime;
        }
    }
}

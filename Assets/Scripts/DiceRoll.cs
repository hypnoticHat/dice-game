using UnityEngine;
using TMPro;

public class DiceRoll : MonoBehaviour
{
    Rigidbody rb;
    bool hasLanded;
    bool thrown;
    Vector3 accelerationDir;
    Vector3 initPosition;


    public bool GetedValue;
    public TMP_Text DiceNum;
    public int diceValue;
    public DiceSide[] diceSides;

    // Update is called once per frame
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initPosition = transform.position;
        rb.useGravity = false;
    }

    void Update()   
    {
        //geting shake phone motion
        accelerationDir = Input.acceleration;

        //if shake >= 5f or geting space key
        if (accelerationDir.sqrMagnitude >=5f || Input.GetKeyDown(KeyCode.Space))
        {
            RollDice();

        }
        //if the dice finish roll and landed
        if(rb.IsSleeping() && !hasLanded && thrown)
        {
            hasLanded = true;
            rb.useGravity = false;
            rb.isKinematic = true;

            sideValueCheck();

        }
        //if something wrong happen when the dice rolling
        else if (rb.IsSleeping() && hasLanded && diceValue== 0)
        {
            Reset();
        }
    }

    void RollDice()
    {
        if(!thrown && !hasLanded)
        {
            thrown = true;
            rb.useGravity = true;
            rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        }
        else if(thrown && hasLanded)
        {
            RollAgain();
        }
    }

    //reset dice posiotion and make it throwable
    private void Reset()
    {
        transform.position = initPosition;
        thrown = false;
        hasLanded = false;
        rb.useGravity = false;
        rb.isKinematic= false;  
    }

    //rollagain
    void RollAgain()
    {
        Reset();
        thrown = true;
        rb.useGravity = true;
        rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
    }

    //check number of each face
    void sideValueCheck()
    {
        diceValue = 0;
        //run thru side and check which one is on ground
        foreach (DiceSide side in diceSides)
        {
            //if face hit grouond and stop the player get values and dice number change
            if (side.OnGround())
            {
                diceValue = side.sideValue;
                DiceNum.text = diceValue.ToString();
                GetedValue = true;
            }
        }
    }
}

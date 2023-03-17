using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public string inputSteerAxis = "Horizontal";
    public string inputThrottleAxis = "Vertical";
    public string inputHandBrake = "space";

    public float ThrottelInput { get; private set; }
    public float SteerInput { get; private set; }
    public bool HandBrake { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ThrottelInput = Input.GetAxis(inputThrottleAxis);
        SteerInput = Input.GetAxis(inputSteerAxis);
        HandBrake = Input.GetKey(inputHandBrake);
    }
}

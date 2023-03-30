using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DrivingInputController : MonoBehaviour
{
    //Car Control Variables
    private PlayerInput playerInput;
    private InputAction driveAction;
    private InputAction handBrakeAction;

    public Transform CenterOfMass;
    public float motorTorque = 2000f;
    public float maxSteer = 30f;
    public float brakeForce = 10000f;

    public float Steer { get; set; }
    public float Throttle { get; set; }
    public float Brake { get; set; }

    private Rigidbody _rigidbody;
    private Wheel[] wheels;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        wheels = GetComponentsInChildren<Wheel>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = CenterOfMass.localPosition;
        driveAction = playerInput.actions["Drive"];
        handBrakeAction = playerInput.actions["Handbrake"];
    }


    // Update is called once per frame
    void Update()
    {   
        Steer = driveAction.ReadValue<Vector2>().x;
        Throttle = driveAction.ReadValue<Vector2>().y;
        Brake = handBrakeAction.ReadValue<float>();
        
        foreach (var wheel in wheels)
        {
            wheel.SteerAngle = Steer * maxSteer;
            wheel.Torque = Throttle * motorTorque;
            if (Brake > 0f)
            {
                wheel.BrakeForce = brakeForce;
            }
            else
            {
                wheel.BrakeForce = 0f;
            }
        }
    }
}

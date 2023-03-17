using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Transform CenterOfMass;
    public float motorTorque = 2000f;
    public float maxSteer = 30f;
    public float brakeForce = 10000f;

    public float Steer { get; set; }
    public float Throttle { get; set; }
    public bool Brake { get; set; }
    

    private Rigidbody _rigidbody;
    private Wheel[] wheels;

    // Start is called before the first frame update
    void Start()
    {
        wheels = GetComponentsInChildren<Wheel>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = CenterOfMass.localPosition;
    }

    // Update is called once per frame
    void Update()
    {   
        Steer = InputManager.driveInput.x;
        Throttle = InputManager.driveInput.y;
        Brake = InputManager.handBrakeInput;
        
        foreach (var wheel in wheels)
        {
            wheel.SteerAngle = Steer * maxSteer;
            wheel.Torque = Throttle * motorTorque;
            if (Brake)
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

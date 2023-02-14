using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    public WheelCollider wheelColliderFrontLeft;
    public WheelCollider wheelColliderFrontRight;
    public WheelCollider wheelColliderBackLeft;
    public WheelCollider wheelColliderBackRight;

    public Transform wheelFrontLeft;
    public Transform wheelFrontRight;
    public Transform wheelBackLeft;
    public Transform wheelBackRight;

    public float motorTorque = 100f;
    public float maxSteer = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        wheelColliderBackLeft.motorTorque = Input.GetAxis("Vertical") * motorTorque;
        wheelColliderBackRight.motorTorque = Input.GetAxis("Vertical") * motorTorque;
    }
}

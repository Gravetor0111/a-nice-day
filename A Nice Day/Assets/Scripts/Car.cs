using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Transform centerOfMass;

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
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    void FixedUpdate()
    {
        wheelColliderFrontLeft.steerAngle = Input.GetAxis("Horizontal") * maxSteer;
        wheelColliderFrontRight.steerAngle = Input.GetAxis("Horizontal") * maxSteer;
        wheelColliderBackLeft.motorTorque = Input.GetAxis("Vertical") * motorTorque;
        wheelColliderBackRight.motorTorque = Input.GetAxis("Vertical") * motorTorque;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = Vector3.zero;
        var ros = Quaternion.identity;

        wheelColliderBackLeft.GetWorldPose(out pos, out ros);
        wheelBackLeft.position = pos;
        wheelBackLeft.rotation = ros;

        wheelColliderBackRight.GetWorldPose(out pos, out ros);
        wheelBackRight.position = pos;
        wheelBackRight.rotation = ros;
        
        wheelColliderFrontLeft.GetWorldPose(out pos, out ros);
        wheelFrontLeft.position = pos;
        wheelFrontLeft.rotation = ros;
        
        wheelColliderFrontRight.GetWorldPose(out pos, out ros);
        wheelFrontRight.position = pos;
        wheelFrontRight.rotation = ros;
    }
}

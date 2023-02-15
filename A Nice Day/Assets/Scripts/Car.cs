using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Transform centerOfMass;
    public float motorTorque = 1500f;
    public float maxSteer = 20f;
    public float Steer { get; set; }
    public float Throttle { get; set; }
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

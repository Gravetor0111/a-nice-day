using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private PlayerInput playerInput;
    private InputAction driveAction;
    private InputAction handBrakeAction;
    

    public static Vector2 driveInput;
    public static bool handBrakeInput;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        driveAction = playerInput.actions["Drive"];
        handBrakeAction = playerInput.actions["Handbrake"];
    }

    // Update is called once per frame
    void Update()
    {
        driveInput = driveAction.ReadValue<Vector2>();
        handBrakeInput = handBrakeAction.triggered;
        if (handBrakeInput)
        Debug.Log(handBrakeInput);
    }
}

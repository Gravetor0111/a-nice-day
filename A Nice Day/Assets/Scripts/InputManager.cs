using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public bool isDriving = true;
    private PlayerInput playerInput;

    //TPP Player Control Variables
    private InputAction moveAction;
    private InputAction sprintAction;
    private InputAction jumpAction;

    //Car Control Variables
    private InputAction driveAction;
    private InputAction handBrakeAction;
    
    //TPP Player Inputs
    public static Vector2 moveInput;
    public static bool jumpInput;
    public static bool sprintInput;

    //Car Inputs
    public static Vector2 driveInput;
    public static bool handBrakeInput;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Movement"];
        jumpAction = playerInput.actions["Jump"];
        sprintAction = playerInput.actions["Sprint"];
        
        driveAction = playerInput.actions["Drive"];
        handBrakeAction = playerInput.actions["Handbrake"];
    }

    // Update is called once per frame
    void Update()
    {
        if (isDriving)
        {
            driveInput = driveAction.ReadValue<Vector2>();
            handBrakeInput = handBrakeAction.triggered;
            if (driveInput != Vector2.zero)
            {
                Debug.Log(driveInput);
            }
        }
        else
        {
            moveInput = moveAction.ReadValue<Vector2>();
            jumpInput = jumpAction.triggered;
            sprintInput = sprintAction.triggered;
        }
    }
}

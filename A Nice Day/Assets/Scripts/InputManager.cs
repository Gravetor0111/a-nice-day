using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;


public class InputManager : MonoBehaviour
{
    
    PlayerTPPScript player1;
    DrivingInputController vehicle1;
    

    private PlayerInput playerInput;

    // Player Actions
    private InputAction moveAction, sprintAction, jumpAction, crouchAction, enterAction, lookAction, aimAction;

    // Vehicle Actions
    private InputAction driveAction, exitAction, handBrakeAction;

    // Game Objects
    public GameObject p1GameObject, v1GameObject = null;

    // Virtual CM Cameras
    public CinemachineVirtualCamera playerCam, aimCam, vehicleCam;
    public static bool inCar;

    // Transform(s)
    public static Transform playerTransform, vehicleTransform;


    // Player Input Variables
    public static Vector2 movementInp, lookInp;
    public static float sprintInp, jumpInp, enterInp, aimInp;
    public static bool crouchInp;

    // Vehicle Input Variables
    public static Vector2 driveInp;
    public static float handBrakeInp, exitInp;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerCam = GameObject.Find("CM TPP Normal").GetComponent<CinemachineVirtualCamera>();
        aimCam = GameObject.Find("CM TPP Aim").GetComponent<CinemachineVirtualCamera>();
        vehicleCam = GameObject.Find("CM Vehicle Normal").GetComponent<CinemachineVirtualCamera>();
        
        // For Player
        moveAction = playerInput.actions["Movement"];
        sprintAction = playerInput.actions["Sprint"];
        jumpAction = playerInput.actions["Jump"];
        crouchAction = playerInput.actions["Crouch"];
        enterAction = playerInput.actions["Enter"];
        lookAction = playerInput.actions["Look"];
        aimAction = playerInput.actions["Aim"];

        // For Vehicle
        driveAction = playerInput.actions["Drive"];
        handBrakeAction = playerInput.actions["Handbrake"];
        exitAction = playerInput.actions["Exit"];
        inCar = false;

        p1GameObject = GameObject.Find("TPPCharacter");
        v1GameObject = GameObject.Find("Car");

        playerTransform = p1GameObject.transform;
        vehicleTransform = v1GameObject.transform;

        player1 = p1GameObject.GetComponentInChildren<PlayerTPPScript>();
        vehicle1 = v1GameObject.GetComponentInChildren<DrivingInputController>();

    }

    // Update is called once per frame
    void Update()
    {
        // Player
        
            movementInp = moveAction.ReadValue<Vector2>();
            sprintInp = sprintAction.ReadValue<float>();
            jumpInp = jumpAction.ReadValue<float>();
            crouchInp = crouchAction.triggered;
            enterInp = enterAction.ReadValue<float>();
            lookInp = lookAction.ReadValue<Vector2>();
            aimInp = aimAction.ReadValue<float>();
        
        
        // Vehicle
        
            driveInp = driveAction.ReadValue<Vector2>();
            handBrakeInp = handBrakeAction.ReadValue<float>();
            exitInp = exitAction.ReadValue<float>();
        
        
        // Debug.Log(driveInp);
        // Debug.Log(handBrakeInp);


        float distance = Vector3.Distance(playerTransform.position, vehicleTransform.position);
    
        // Debug.Log("Player object: " + player1);
        // Debug.Log("Vehicle object: " + vehicle1);
        // Debug.Log("IN CAR: " + inCar);
        // Debug.Log("Enter INP: " + enterInp);
        // Debug.Log("Exit INP: " + exitInp);
        // Debug.Log("Current Action: " + playerInput.actions);

        
        if (distance <= player1.carInteractionDistance && enterInp > 0f && !inCar)
        {
            player1.HopIn();
            playerInput.SwitchCurrentActionMap("Car");
        }

        if (inCar && exitInp > 0f)
        {
            vehicle1.GetOut();
            playerInput.SwitchCurrentActionMap("Player");
        }
    }

    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

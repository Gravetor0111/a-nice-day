using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    GameObject p1GameObject, v1GameObject = null;
    PlayerTPPScript player1;
    DrivingInputController vehicle1;

    private PlayerInput playerInput;
    // Player Controls
    private InputAction moveAction, sprintAction, jumpAction, enterAction, exitAction;

    // Vehicle Controls
    private InputAction driveAction, handBrakeAction;

    public static bool inCar;

    // Transform(s)
    public static Transform playerTransform, vehicleTransform;


    // Player Input Variables
    public static Vector2 movementInp;
    public static float sprintInp, jumpInp, enterInp, exitInp;

    // Vehicle Input Variables
    public static Vector2 driveInp;
    public static float handBrakeInp;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        player1 = GetComponent<PlayerTPPScript>();
        vehicle1 = GetComponent<DrivingInputController>();
        
        // For Player
        moveAction = playerInput.actions["Movement"];
        jumpAction = playerInput.actions["Jump"];
        sprintAction = playerInput.actions["Sprint"];
        enterAction = playerInput.actions["Enter"];
        exitAction = playerInput.actions["Exit"];

        // For Vehicle
        driveAction = playerInput.actions["Drive"];
        handBrakeAction = playerInput.actions["Handbrake"];
        inCar = false;

        p1GameObject = GameObject.Find("TPPCharacter");
        v1GameObject = GameObject.Find("Car");

        playerTransform = p1GameObject.transform;
        vehicleTransform = v1GameObject.transform;

    }

    // Update is called once per frame
    void Update()
    {
        // Player
        movementInp = moveAction.ReadValue<Vector2>();
        jumpInp = jumpAction.ReadValue<float>();
        sprintInp = sprintAction.ReadValue<float>();
        enterInp = enterAction.ReadValue<float>();
        exitInp = exitAction.ReadValue<float>();

        // Vehicle
        driveInp = moveAction.ReadValue<Vector2>();
        handBrakeInp = jumpAction.ReadValue<float>();

        Debug.Log(movementInp);
        Debug.Log(jumpInp);
        Debug.Log(sprintInp);
        Debug.Log(driveInp);


        float distance = Vector3.Distance(playerTransform.position, vehicleTransform.position);
        
        if (distance <= player1.carInteractionDistance && enterInp > 0f && !inCar)
        {
            Debug.Log("Entering Car");
            player1.HopIn();
            p1GameObject.SetActive(false);

        }

        if (inCar && exitInp > 0f)
        {
            Debug.Log("Exiting Car");
            player1.GetOut();
            p1GameObject.SetActive(true);
            
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerTPPScript : MonoBehaviour
{
    private PlayerInput playerInput;
    //TPP Player Control Variables
    private InputAction moveAction, sprintAction, jumpAction, enterExitAction;
    private Animator anim;
    private CharacterController controller;
    private GameObject triggerVolume;
    private Vector3 playerVelocity;
    private Transform sphereTransform;
    // private GameObject currentCar = null;
    private bool groundedPlayer;
    // private bool inCar;
    

    [SerializeField]
    private float playerSpeed = 14.0f;
    [SerializeField]
    private float jumpHeight = 2.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    // private float carInteractionDistance = 10f;

    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        sphereTransform = transform.Find("TriggerSphere");
        SphereCollider sphereTrigger = sphereTransform.GetComponentInChildren<SphereCollider>();
        moveAction = playerInput.actions["Movement"];
        jumpAction = playerInput.actions["Jump"];
        sprintAction = playerInput.actions["Sprint"];
        enterExitAction = playerInput.actions["EnterExit"];
    }

    void Update()
    {
        groundedPlayer = IsGrounded();
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(moveAction.ReadValue<Vector2>().x, 0, moveAction.ReadValue<Vector2>().y);
        if (move != Vector3.zero)
        {
            Walk(move);
            if (sprintAction.ReadValue<float>() > 0 && move != Vector3.zero)
            {
                Run(move);
            }
        }

        if (jumpAction.ReadValue<float>() > 0f && groundedPlayer)
        {
            Jump();
        }
        
        

        // Changes the height position of the player..
        

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (enterExitAction.ReadValue<float>() > 0f)
        {
            Debug.Log("Drive Car");
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Vehicle")
        {
            Debug.Log("Now you can enter");
            Debug.Log(sphereTransform);
        }
    }






    private bool IsGrounded() 
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f)) 
        {
            if (hit.collider.CompareTag("Ground")) 
            {
                return true;
            }
        }
        return false;
    }

    private void Walk(Vector3 move)
    {
        gameObject.transform.forward = move;
        controller.Move(move * Time.deltaTime * playerSpeed);
    }

    private void Run(Vector3 move)
    {
        controller.Move(move * Time.deltaTime * (playerSpeed * 2));
    }

    private void Jump()
    {
        Debug.Log("Jump");
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }
}

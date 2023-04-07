using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerTPPScript : MonoBehaviour
{   
    private Animator anim;
    private CharacterController controller;
    private GameObject triggerVolume;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool isWalking;
    private bool isRunning;

    // Sphere Collider
    private Transform sphereTransform;
    

    [SerializeField]
    private float playerSpeed = 14.0f;
    [SerializeField]
    private float jumpHeight = 2.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    public float carInteractionDistance = 10f;

    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        
        // Car related stuff
        sphereTransform = transform.Find("TriggerSphere");
        SphereCollider sphereTrigger = sphereTransform.GetComponentInChildren<SphereCollider>();    
    }

    void Update()
    {
        groundedPlayer = IsGrounded();
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(InputManager.movementInp.x, 0, InputManager.movementInp.y);

        if (move == Vector3.zero)
        {
            Stand(move);
        }

        if (move != Vector3.zero)
        {
            Walk(move);
            if (InputManager.sprintInp > 0 && move != Vector3.zero)
            {
                Run(move);
            }
        }

        if (InputManager.jumpInp > 0f && groundedPlayer)
        {
            Jump();
        }
        // Changes the height position of the player..
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        anim.SetBool("IsWalking", isWalking);
        anim.SetBool("IsRunning", isRunning);
    }

    

    

    // OTHER METHODS
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

    private void Stand(Vector3 move)
    {
        isWalking = false;
        isRunning = false;
    }


    private void Walk(Vector3 move)
    {
        gameObject.transform.forward = move;
        isWalking = true;
        isRunning = false;
        controller.Move(move * Time.deltaTime * playerSpeed);
    }

    private void Run(Vector3 move)
    {
        isWalking = false;
        isRunning = true;
        controller.Move(move * Time.deltaTime * (playerSpeed * 2));
    }

    private void Jump()
    {
        Debug.Log("Jump");
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }

    public void HopIn()
    {
        transform.SetPositionAndRotation(GameObject.Find("SeatLocation").transform.position, GameObject.Find("SeatLocation").transform.rotation);
        InputManager.inCar = true;
        Debug.Log("IS IN THE CAR NOW");
    }

    public void GetOut()
    {
        transform.SetPositionAndRotation((InputManager.vehicleTransform.position - InputManager.vehicleTransform.TransformDirection(Vector3.left)), InputManager.vehicleTransform.rotation);
        InputManager.inCar = false;
        Debug.Log("HAS LEFT THE CAR");
    }
}

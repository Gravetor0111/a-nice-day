using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerTPPScript : MonoBehaviour
{   
    float velocity = 0.0f;
    public float acceleration = 0.1f;
    public float deceleration = 0.5f;
    int velocityHash;

    private Animator anim;
    private CharacterController controller;
    private GameObject triggerVolume, gameManagerObj;
    private Vector3 playerVelocity;
    private InputManager imObject;
    private bool groundedPlayer, isWalking, isRunning, isJumping, isCrouching, isCrawling, isAiming;
    private Transform sphereTransform, cameraTransform;
    
    [SerializeField]
    private float playerSpeed = 14.0f;
    [SerializeField]
    private float jumpHeight = 2.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 10f;

    public float carInteractionDistance = 10f;
    public GameObject lookReticle;
    public GameObject aimReticle;

    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        velocityHash = Animator.StringToHash("Velocity");

        // Car related stuff
        sphereTransform = transform.Find("TriggerSphere");
        SphereCollider sphereTrigger = sphereTransform.GetComponentInChildren<SphereCollider>();
        gameManagerObj = GameObject.Find("GameManager");
        imObject = gameManagerObj.GetComponentInChildren<InputManager>();
    }

    void Update()
    {
        groundedPlayer = IsGrounded();
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 moveDirection = new Vector3(InputManager.lookInp.x, 0, InputManager.lookInp.y);
        Vector3 move = new Vector3(InputManager.movementInp.x, 0, InputManager.movementInp.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;

        float inputMagnitude = Mathf.Clamp01(moveDirection.magnitude);

        if (move == Vector3.zero)
        {
            Stand(move);
            CharacterLookAt();
            if (InputManager.crouchInp)
            {
                CrouchIdle(move);
                CharacterLookAt();
            }
        }       

        if (move != Vector3.zero)
        {
            Walk(move);
            if (InputManager.sprintInp > 0 && !isAiming)
            {
                Run(move);
            }
            if (InputManager.crouchInp)
            {
                Debug.Log("IS CRAWLING");
                Crawl(move);
            }
        }


        if (InputManager.jumpInp > 0f && groundedPlayer)
        {
            Jump();
        }
        // Changes the height position of the player..
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (InputManager.aimInp > 0f)
        {
            Aim();
        }
        else
        {
            Miss();
        }

        anim.SetFloat(velocityHash, velocity);
        // anim.SetBool("isWalking", isWalking);
        // anim.SetBool("isRunning", isRunning);
        // anim.SetBool("IsJumping", isJumping);
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
        // isWalking = false;
        // isRunning = false;
        // isJumping = false;
        if (velocity > 0.0f)
        {
            velocity -= Time.deltaTime * deceleration;
        }
        if (velocity < 0.0f)
        {
            velocity = 0.0f;
        }
    }

    private void CrouchIdle(Vector3 move)
    {
        isCrouching = true;
        isWalking = false;
        isRunning = false;
        //isJumping = false;
        isCrawling = false;
    }

    private void Walk(Vector3 move)
    {
        gameObject.transform.forward = move;
        // isWalking = true;
        // isRunning = false;
        // isJumping = false;
        Debug.Log(velocity);
        controller.Move(move * Time.deltaTime * playerSpeed);
        if (velocity < 1.0f)
        {
            velocity += Time.deltaTime * acceleration;
        }
    }

    private void Run(Vector3 move)
    {
        isWalking = false;
        isCrouching = false;
        isRunning = true;
        // isJumping = false;
        controller.Move(move * Time.deltaTime * (playerSpeed * 3));
    }

    private void Crawl(Vector3 move)
    {
        Debug.Log("CRAWL METOD IS BEING CALLED!");
        gameObject.transform.forward = move;
        isCrouching = false;
        isWalking = false;
        isRunning = false;
        //isJumping = false;
        isCrawling = true;
        controller.Move(move * Time.deltaTime * playerSpeed);
    }

    private void Jump()
    {
        isWalking = false;
        isRunning = false;
        isCrawling = false;
        isCrouching = false;
        //isJumping = true;
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }

    public void HopIn()
    {
        //transform.SetPositionAndRotation(GameObject.Find("SeatLocation").transform.position, GameObject.Find("SeatLocation").transform.rotation);
        InputManager.inCar = true;
        imObject.vehicleCam.Priority = 3;
        imObject.playerCam.Priority = 1;
        imObject.p1GameObject.SetActive(false);
    }

    private void CharacterLookAt()
    {
        // Rotating character
        Quaternion playerRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, playerRotation, rotationSpeed * Time.deltaTime);
    }
    private void Aim()
    {
        imObject.aimCam.Priority = 4;
        isAiming = true;
        aimReticle.SetActive(isAiming);
        lookReticle.SetActive(!isAiming);
    }
    private void Miss()
    {
        imObject.aimCam.Priority = 2;
        isAiming = false;
        aimReticle.SetActive(isAiming);
        lookReticle.SetActive(!isAiming);
    }
}

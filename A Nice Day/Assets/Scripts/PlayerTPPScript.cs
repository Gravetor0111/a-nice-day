using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerTPPScript : MonoBehaviour
{
    private PlayerInput playerInput;
    //TPP Player Control Variables
    private InputAction moveAction;
    private InputAction sprintAction;
    private InputAction jumpAction;

    private Animator anim;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [SerializeField]
    private float playerSpeed = 3.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Movement"];
        jumpAction = playerInput.actions["Jump"];
        sprintAction = playerInput.actions["Sprint"];
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(moveAction.ReadValue<Vector2>().x, 0, moveAction.ReadValue<Vector2>().y);
        controller.Move(move * Time.deltaTime * playerSpeed);
        
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            
            if (sprintAction.ReadValue<float>() > 0 && move != Vector3.zero)
            {
                
                controller.Move(move * Time.deltaTime * (playerSpeed * 2));;
            }
            
        }
        
        // Changes the height position of the player..
        if (jumpAction.ReadValue<float>() > 0f && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}

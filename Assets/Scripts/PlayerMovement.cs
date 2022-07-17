using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("keybinds")]
    public KeyCode SprintKey = KeyCode.LeftShift;
    public KeyCode JumpKey = KeyCode.Space;

    public CharacterController characterController;

    [Header ("physics")]
    public float gravity = -9.81f;
    private float Speed;
    public float WalkingSpeed;
    public float SprintSpeed;

    

    public float JumpHeight = 3f;
    bool isGrounded;
    public Transform GroundCheck;
    public float GroundDisstance = 0.4f;
    public LayerMask GroundMask;

    

    Vector3 velocity;

    

    public MovementState stateplayer;
    public enum MovementState
    {
        Walking,
        Sprinting,
        Air,
    }


    private void StateHandler()
    {
        //mode - sprinting
        if (Input.GetKey(SprintKey) && isGrounded)
        {
            stateplayer = MovementState.Sprinting;
            Speed = SprintSpeed;
        }
        else if (isGrounded)
        {
            stateplayer = MovementState.Walking;
            Speed = WalkingSpeed;
        }
        else
        {
            stateplayer = MovementState.Air;
        }
    }
    
    void MovementPlayer()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * Speed * Time.deltaTime);

        //jumping
        if (Input.GetKey(JumpKey) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }

    }

    void GroundChecks()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDisstance, GroundMask);
        //checking if you are falling
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //falling speed
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

    }






    // Update is called once per frame
    void FixedUpdate()
    {
              
        StateHandler();
        MovementPlayer();
        GroundChecks();
                 
    }
}

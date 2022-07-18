using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("keybinds")]
    public KeyCode SprintKey = KeyCode.LeftShift;
    public KeyCode JumpKey = KeyCode.Space;
    public KeyCode CrouchKey = KeyCode.LeftControl;


    public CharacterController characterController;

    [Header ("physics")]
    public float gravity;
    private float Speed;
    public float WalkingSpeed;
    public float SprintSpeed;

    Rigidbody rb;

    public float JumpHeight = 3f;
    bool isGrounded;
    bool isCeiling = false;
    public float CeilingDisstance = 0.4f;
    public Transform CeilingCheck;
    public Transform GroundCheck;
    public float GroundDisstance = 0.4f;
    public LayerMask GroundMask;


    [Header("Crouching")]
    public float CrouchSpeed;
    public float CrouchYScale;
    private float StartYScale;



    Vector3 velocity;

    

    public MovementState stateplayer;
    public enum MovementState
    {
        Walking,
        Sprinting,
        Crouching,
        Air,
    }

    private void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        StartYScale = transform.localScale.y;
    }
    private void StateHandler()
    {




        //checking if able to uncrouch
        if (stateplayer == MovementState.Crouching)
        {
            isCeiling = Physics.CheckSphere(CeilingCheck.position, CeilingDisstance, GroundMask);
        }

        //mode - crouching
        if (Input.GetKey(CrouchKey) || isCeiling == true)
        {

          
            stateplayer = MovementState.Crouching;
            Speed = CrouchSpeed;
            transform.localScale = new Vector3(transform.localScale.x, CrouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        //mode - sprinting
        else if (Input.GetKey(SprintKey) && isGrounded && stateplayer!= MovementState.Crouching)
        {
            stateplayer = MovementState.Sprinting;
            Speed = SprintSpeed;
        }
        else if (isGrounded)
        {
            stateplayer = MovementState.Walking;
            Speed = WalkingSpeed;
            transform.localScale = new Vector3(transform.localScale.x, StartYScale, transform.localScale.z);
        }
        else
        {
            stateplayer = MovementState.Air;
        }
       
    }
    
    void MovementPlayer()
    {

        float xmovement = Input.GetAxis("Horizontal");
        float zmovement = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xmovement + transform.forward * zmovement;

        characterController.Move(move * Speed * Time.deltaTime);
        
    
        //jumping
        if (Input.GetKey(JumpKey) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }
        //crouching
       
       
        
       

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
    void Update()
    {
        GroundChecks();
        StateHandler();
       
        MovementPlayer();
        



    }
}

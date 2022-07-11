using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController characterController;


    public float gravity = -9.81f;
    public float speed = 12f;
    public float JumpHeight = 3f; 

    public Transform GroundCheck;
    public float GroundDisstance = 0.4f;
    public LayerMask GroundMask;


    Vector3 velocity;

    bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDisstance, GroundMask);
        //checking if you are falling
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }



        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        
        characterController.Move(move * speed * Time.deltaTime);

        //jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }

        //falling speed
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);    
    }
}

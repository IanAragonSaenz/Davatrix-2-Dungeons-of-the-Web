using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float crouchSpeed = 6f;
    public float walkSpeed = 12f;
    public float runSpeed = 24f;
    public CharacterController controller;
    
    [Space]
    
    [Header("Air Mechanics")]
    public float gravity = -9.81f;
    public float jumpHeight = 5f;

    [Space]

    [Header("Ground Checking")]
    public Transform groundCheck;
    public float groundDistance = .25f;
    public float groundDistance2 = .4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool grounded;
    private bool crouched;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            crouched = true;
            transform.localScale = new Vector3(transform.localScale.x, .5f, transform.localScale.z);
        }
        else
        {
            crouched = false;
            transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
        }

        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) || Physics.CheckSphere(groundCheck.position, groundDistance2, groundMask);
        if (grounded && gravity < 0) velocity.y = -3f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (Input.GetKey(KeyCode.LeftShift) && !crouched) move *= runSpeed;
        else if (crouched) move *= crouchSpeed;
        else move *= walkSpeed;
        controller.Move(move * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && grounded) velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}

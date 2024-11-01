using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // The speed at which the player moves
    public float rotationSpeed = 100f; // The speed at which the player rotates
    public float jumpHeight = 2f; // The height of the player's jump
    public float gravity = -9.81f; // The force of gravity applied to the player
    public float groundDistance = 0.4f; // The radius of the sphere used to check for ground
    public LayerMask groundMask; // The layer(s) representing the ground
    public Transform groundCheck; // The position of the sphere used to check for ground

    public Animator animator;
    private CharacterController controller; // The character controller component
    private Vector3 velocity; // The current velocity of the player
    private bool isGrounded; // Whether the player is grounded
    private float horizontalInput; // The horizontal input axis
    private float verticalInput; // The vertical input axis

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Set the "IsMoving" parameter based on whether the player is moving
        if (horizontalInput != 0f || verticalInput != 0f)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        animator.SetFloat("Forward", verticalInput);

        Vector3 moveDirection = transform.forward * verticalInput * moveSpeed;
        controller.Move(moveDirection * Time.deltaTime);

        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("Jump");
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}

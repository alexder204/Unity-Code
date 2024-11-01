using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerDialogue
{
    public class TopDownMovement : MonoBehaviour
    {
        public float moveSpeed = 2f;
        public float sprintSpeed = 2f;
        public float currentSpeed = 2f;
        public Rigidbody2D rb;
        public Animator animator;
        private Vector2 movement;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            if (Input.GetButtonDown("Sprint"))
            {
                currentSpeed = moveSpeed + sprintSpeed;
            }
            if (Input.GetButtonUp("Sprint"))
            {
                currentSpeed = moveSpeed;
            }
        }

        void FixedUpdate()
        {
            rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
        }
    }
}

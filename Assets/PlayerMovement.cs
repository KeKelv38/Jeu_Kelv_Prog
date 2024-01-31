using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_character : MonoBehaviour
{
    public float moveSpeed;
    public float jumpforce;
    public bool isJumping = false;
    public bool isGrounded;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;

    public Animator animator;


    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        MovePlayer(horizontalMovement);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", rb.velocity.x);    
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if(isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpforce));
            isJumping = false;
        }
    }
}

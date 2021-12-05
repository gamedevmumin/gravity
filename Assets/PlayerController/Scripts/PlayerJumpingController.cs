using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class PlayerJumpingController : MonoBehaviour, IJumpingController
{
    [SerializeField]
    float jumpPressedRemember = 0.2f;
    float jumpPressedRememberTimer;
    [SerializeField]
    float groundedRemember = 0.2f;
    float groundedRememberTimer;

    Rigidbody2D rb;
    [SerializeField] [Range(0, 1)]
    float cutOfJumpHeight = 0.85f;
    IGroundedChecking groundedChecker;
    [SerializeField]
    private float jumpHeight;
    [SerializeField] private float wallJumpTime = 0.1f;
    [SerializeField] private Rigidbody2D[] rbs;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundedChecker = GetComponent<IGroundedChecking>();
    }

    public void ManageJumping()
    {
        groundedRememberTimer -= Time.deltaTime;
        if (groundedChecker.IsGrounded())
        {
            Debug.Log(groundedChecker.IsGrounded());
            groundedRememberTimer = groundedRemember;
        }

        jumpPressedRememberTimer -= Time.deltaTime;

            if (Input.GetButtonDown("Jump"))
                jumpPressedRememberTimer = jumpPressedRemember;

            if (groundedRememberTimer > 0f && jumpPressedRememberTimer > 0f)
            {
                Debug.Log("triedToJump");
                jumpPressedRememberTimer = 0f;
                groundedRememberTimer = 0f;
                foreach (var rb in (rbs))
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                }
            } 
            
            if (Input.GetButtonUp("Jump"))
            {
                foreach (var rb in (rbs))
                {
                    if (rb.velocity.y > 0f)
                        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * cutOfJumpHeight);
                }
            }
    }
}



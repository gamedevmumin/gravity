using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedChecker : MonoBehaviour, IGroundedChecking
{
    [SerializeField]
    LayerMask whatIsGround;
    [SerializeField]
    GameObject groundCheck;

    [SerializeField] private GameObject upGroundCheck;
    [SerializeField] private GameObject downGroundCheck;
    bool isGrounded = true;
    [SerializeField] private GravityInfo gravityInfo;
    void Start()
    {
        if (groundCheck == null) Debug.LogError("groundCheck variable isn't set");
    }

    void FixedUpdate()
    {
        groundCheck = gravityInfo.gravityDirection == -1 ? downGroundCheck : upGroundCheck;
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, 0.2f, whatIsGround);
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}
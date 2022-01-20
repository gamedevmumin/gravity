using UnityEngine;

/**
 * class representing player jumping controller
 */
public class PlayerJumpingController : MonoBehaviour, IJumpingController
{
    [SerializeField]
    float jumpPressedRemember = 0.2f;
    float jumpPressedRememberTimer;
    [SerializeField]
    float groundedRemember = 0.2f;
    float groundedRememberTimer;
    
    IGroundedChecking groundedChecker;
    [SerializeField]
    private float jumpHeight;
    [SerializeField] private Rigidbody2D[] rbs;
    [SerializeField] private GravityInfo gravityInfo;
    
    private void Start()
    {
        groundedChecker = GetComponent<IGroundedChecking>();
    }

    /**
     * method manages jumping of player - it awaits for player to press button and sets his velocity
     * to simulate jumping
     */
    public void ManageJumping()
    {
        groundedRememberTimer -= Time.deltaTime;
        if (groundedChecker.IsGrounded())
        {
            groundedRememberTimer = groundedRemember;
        }

        jumpPressedRememberTimer -= Time.deltaTime;

            if (Input.GetButtonDown("Jump"))
                jumpPressedRememberTimer = jumpPressedRemember;

            if (groundedRememberTimer > 0f && jumpPressedRememberTimer > 0f)
            {
                AudioManager.Instance.PlaySound("Jump");
                jumpPressedRememberTimer = 0f;
                groundedRememberTimer = 0f;
                foreach (var rb in (rbs))
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpHeight*-gravityInfo.gravityDirection);
                }
            }
    }
}



using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public DeathOverlayFader deathOverlay;

    public Rigidbody2D rb2D;

    private FloorController currentFloor;

    public float moveSpeed;
    public float jumpForce;
    private bool isJumping;
    private bool isOnPlatform;
    private float moveHorizontal;
    private float moveVertical;
    private bool _faceRight;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentFloor = GameObject.Find("Floor0")
            .GetComponent<FloorController>();
        isJumping = false;
        _faceRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (moveHorizontal != 0)
        {
            if (moveHorizontal < 0 && _faceRight)
            {
                Flip();
            }
            else if (moveHorizontal > 0 && !_faceRight)
            {   
                Flip();
            }
            
            animator.SetBool("IsRunning", true);
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0), ForceMode2D.Impulse);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        if (isOnPlatform && !isJumping && moveVertical > 0)
        {
            isJumping = true;
            animator.SetBool("IsJumping", true);
            rb2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("PLAYER COLLIDED WITH: " + col.gameObject.name);
        if (col.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = true;

            if (isJumping)
            {
                isJumping = false;
                animator.SetBool("IsJumping", false);
            }
        }
        else if (col.gameObject.CompareTag("Zilla"))
        {
            deathOverlay.Death();
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        {
            if (col.gameObject.CompareTag("Platform"))
            {
                isJumping = true;
                animator.SetBool("IsJumping", true);
            }
        }
    }

    private void Flip()
    {
        _faceRight = !_faceRight;
        transform.Rotate(0f, 180f, 0f);
    }

    // Called on respawn to reset how the player looks
    public void ResetPlayer()
    {
        // We don't need to Flip() because the death overlay already sets
        // rotation to 0 when it sets the position (maybe do that here?)
        _faceRight = false;
    }
}

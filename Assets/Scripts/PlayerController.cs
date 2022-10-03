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
    private AudioSource jumpAudio;

    private bool isBeingSwallowed;
    private bool movingSceneStarted;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentFloor = GameObject.Find("Floor0")
            .GetComponent<FloorController>();
        isJumping = false;
        _faceRight = false;
        jumpAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        animator.SetBool("IsJumping", rb2D.velocity.y > 0.01);
        animator.SetBool("IsFalling", rb2D.velocity.y < -0.01);
        animator.SetBool("IsRunning", moveHorizontal != 0);

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
            
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0), ForceMode2D.Impulse);
        }

        if (isOnPlatform && !isJumping && moveVertical > 0)
        {
            isJumping = true;
            rb2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpAudio.Play();
        }
    }

    public void setIsBeingSwallowed(){
        isBeingSwallowed = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("PLAYER COLLIDED WITH: " + col.gameObject.name);
        if (col.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = true;
            isJumping = false;
        }
        else if (col.gameObject.CompareTag("Zilla"))
        {
            if (isBeingSwallowed) 
            {
                if (!movingSceneStarted)
                {
                    movingSceneStarted = true;
                    print("Move Scene");
                    //TODO: Move scene here. Make sure we reset isBeingSwallowed after moving. Also note - this happens twice (once per chomper)
                }
            } else 
            {
                deathOverlay.Death();
            }
        }
        else if (col.gameObject.CompareTag("Hazard"))
        {
            deathOverlay.Death();
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            isJumping = true;
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
        isBeingSwallowed = false;
    }
}

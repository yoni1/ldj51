using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public DeathOverlayFader deathOverlay;

    private Rigidbody2D rb2D;

    private FloorController currentFloor;

    private float moveSpeed;
    private float jumpForce;
    private bool isJumping;
    private float moveHorizontal;
    private float moveVertical;

    private GameObject player;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentFloor = GameObject.Find("Floor0")
            .GetComponent<FloorController>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        moveSpeed = 3f;
        jumpForce = 50f;
        isJumping = false;
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
            animator.SetBool("IsRunning", true);

            if (moveHorizontal < 0)
            {
                gameObject.transform.localScale = new Vector2(1, 1);
            }
            else
            {
                gameObject.transform.localScale = new Vector2(-1, 1);
            }

            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0), ForceMode2D.Impulse);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        if (!isJumping && moveVertical > 0)
        {
            rb2D.AddForce(new Vector2(0, moveVertical * jumpForce), ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.CompareTag("Platform"))
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
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

    public void SetFloor(FloorController floor)
    {
        currentFloor = floor;
    }

    public void resetCurrentFloor()
    {
        currentFloor.ResetPositions();
    }
}

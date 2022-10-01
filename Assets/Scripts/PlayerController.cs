using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;

    private float moveSpeead;
    private float jumpForce;
    private bool isJumping;
    private float moveHorizontal;
    private float moveVertical;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        
        moveSpeead = 3f;
        jumpForce = 60f;
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
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeead, 0), ForceMode2D.Impulse);
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
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        {
            if (col.gameObject.CompareTag("Platform"))
            {
                isJumping = true;
            }
        }
    }
}

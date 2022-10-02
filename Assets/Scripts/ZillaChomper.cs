using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZillaChomper : MonoBehaviour
{
    private bool isChomping = false;
    private Rigidbody2D rigidBody;

    public int directionMultiplier;
    public int speed = 6000000;

    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isChomping)
        {
            rigidBody.AddForce(new Vector2(directionMultiplier * speed, 0f),
                ForceMode2D.Force);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Chomper collided with: " + collision.gameObject.name);
        if (collision.collider.CompareTag("Zilla"))
        {
            isChomping = false;
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            collision.gameObject.SetActive(false);
        }
    }

    public void Chomp()
    {
        isChomping = true;
    }

    public void StopChomping()
    {
        isChomping = false;
    }
}

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zilla"))
        {
            isChomping = false;
        }
    }

    public void Chomp()
    {
        isChomping = true;
    }
}

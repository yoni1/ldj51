using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeController : MonoBehaviour
{
    public bool facingLeft = true;
    public float punchPower = 10f;

    private void Start()
    {
        if (!facingLeft)
        {
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Zilla"))
        {
            gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Player"))
        {
            float actualPunch;
            if (!facingLeft)
            {
                actualPunch = punchPower;
            }
            else
            {
                actualPunch = -punchPower;
            }
            print("About to punch with: " + actualPunch);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(
                new Vector2(actualPunch, 0), ForceMode2D.Impulse);
        }
    }
}
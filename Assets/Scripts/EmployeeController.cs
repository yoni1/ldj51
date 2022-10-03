using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeController : MonoBehaviour
{
    public bool facingLeft = true;
    public float punchPower = 10f;
    public ItemManager itemManager;

    private static readonly Vector2 UP_NORMAL = new Vector2(0, 1);

    private void Start()
    {
        if (!facingLeft)
        {
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void punchPlayer(Collider2D collision)
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
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(
            new Vector2(actualPunch, 0), ForceMode2D.Impulse);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Zilla"))
    //    {
    //        gameObject.SetActive(false);
    //    }
    //    else if (collision.CompareTag("Player")) {
    //        if (!itemManager.hasStepOnPpl)
    //        {
    //            punchPlayer(collision);
    //            return;
    //        }

    //        ContactPoint2D contacts = new ContactPoint2D[collision.]
    //                foreach (ContactPoint2D contact in collision.GetContacts())
    //        {
    //            if ()
    //                }
    //        punchPlayer(collision);
    //    }

    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || !itemManager.hasStepOnPpl)
        {
            return;
        }

        Vector2 employeePos = new Vector2(transform.position.x, transform.position.y);
        float topOfEmployee = transform.position.y + (GetComponent<SpriteRenderer>().size.y / 2);

        bool isHeadCollisionInput = Mathf.Abs(
            collision.ClosestPoint(transform.position).y - topOfEmployee) < 0.01f;

        bool isHeadCollisionSelf = Mathf.Abs(
            GetComponent<Collider2D>().ClosestPoint(collision.transform.position).y - topOfEmployee) < 0.01f;

        if (isHeadCollisionSelf)
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(
                new Vector2(0, Mathf.Sqrt(2) * playerController.jumpForce),
                ForceMode2D.Impulse);
            GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            punchPlayer(collision);
        }
    }
}
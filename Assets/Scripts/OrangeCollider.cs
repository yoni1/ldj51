using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeCollider : MonoBehaviour
{
    public GameObject currentVCamera;
    public GameObject nextVCamera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentVCamera.SetActive(false);
            nextVCamera.SetActive(true);
        }
    }
}

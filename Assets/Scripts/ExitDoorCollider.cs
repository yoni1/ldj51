using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorCollider : MonoBehaviour
{
    public VirtualCameraController vCamController;
    public GameObject teleportDestination;
    public GameObject player;
    public ZillaBrain zillaBrain;
    private bool isTriggering;

    private void Start()
    {
        isTriggering = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTriggering && collision.CompareTag("Player"))
        {
            vCamController.NextFloor();
            isTriggering = true;
            player.transform.position = teleportDestination.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isTriggering && collision.CompareTag("Player"))
        {
            isTriggering = false;
        }
    }
}

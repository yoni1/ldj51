using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorCollider : MonoBehaviour
{
    public VirtualCameraController vCamController;
    public GameObject player;
    public ZillaBrain zillaBrain;
    private bool isTriggering;
    private bool activated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggering = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggering = false;
        }
    }

    private void Update()
    {
        if (!activated && isTriggering && Input.GetKeyDown("space"))
        {
            activated = true;
            UseExitDoor();
        }   
    }

    private void UseExitDoor()
    {
        vCamController.NextFloor();
        Vector3 teleportDestination = zillaBrain.GetNextSpawnLocation();
        player.transform.position = teleportDestination;
        // player.GetComponent<PlayerController>().SetFloor(
        //     teleportDestination.transform.parent.gameObject.
        //     GetComponent<FloorController>());
        zillaBrain.Chomp();
    }
}

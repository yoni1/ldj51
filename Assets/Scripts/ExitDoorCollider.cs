using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorCollider : MonoBehaviour
{
    public VirtualCameraController vCamController;
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
            Vector3 teleportDestination = zillaBrain.GetNextSpawnLocation();
            player.transform.position = teleportDestination;
            // player.GetComponent<PlayerController>().SetFloor(
            //     teleportDestination.transform.parent.gameObject.
            //     GetComponent<FloorController>());
            zillaBrain.Chomp();
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

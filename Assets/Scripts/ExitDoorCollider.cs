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
        if (gameObject.name != "SceneExitDoor") {
            vCamController.NextFloor();
            player.transform.position = zillaBrain.GetNextSpawnLocation();
        }
        zillaBrain.Chomp();
    }
}

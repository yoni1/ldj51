using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleController : MonoBehaviour
{
    public ZillaBrain zillaBrain;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            zillaBrain.Chomp();
            //deathOverlay.Play("DeathOverlayFadeIn");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOverlayFader : MonoBehaviour
{
    public float fadeInTime;
    public float fadeOutTime;
    public ZillaBrain zillaBrain;

    public PlayerController player;

    private AudioSource deathAudio;

    private void Start()
    {
        deathAudio = GetComponent<AudioSource>();
    }

    IEnumerator fadeIn(SpriteRenderer sprite)
    {
        //print("Doing dem fade in");
        deathAudio.Play();
        Color tmpColor = sprite.color;
        tmpColor.a = 0;
        while (tmpColor.a < 1f)
        {
            tmpColor.a += 1f * Time.deltaTime / fadeInTime;
            sprite.color = tmpColor;
            if (tmpColor.a >= 1f)
            {
                tmpColor.a = 1f;
            }
            yield return null;
        }
        //print("Done with the fadein");
        sprite.color = tmpColor;
        Invoke("DoFadeOut", 1f);
    }

    IEnumerator fadeOut(SpriteRenderer sprite)
    {
        Color tmpColor = sprite.color;
        tmpColor.a = 1f;
        while (tmpColor.a > 0f)
        {
            tmpColor.a -= 1f * Time.deltaTime / fadeOutTime;
            sprite.color = tmpColor;
            if (tmpColor.a <= 0f)
            {
                tmpColor.a = 0f;
            }
            yield return null;
        }
        //print("Done with the fadeout");
        player.gameObject.SetActive(true);
        sprite.color = tmpColor;
    }

    private void DoFadeOut()
    {
        zillaBrain.resetZilla();

        player.transform.SetLocalPositionAndRotation(zillaBrain.GetCurrentSpawnLocation(), Quaternion.identity);       

        zillaBrain.GetFloorController().ResetPositions();
        
        StartCoroutine(fadeOut(GetComponent<SpriteRenderer>()));
        player.ResetPlayer();
    }

    public void Death()
    {
        player.gameObject.SetActive(false);
        StartCoroutine(fadeIn(GetComponent<SpriteRenderer>()));
    }


}

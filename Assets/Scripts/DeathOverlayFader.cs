using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOverlayFader : MonoBehaviour
{
    public float fadeInTime;
    public float fadeOutTime;
    public ZillaBrain zillaBrain;

    public GameObject player;

    IEnumerator fadeIn(SpriteRenderer sprite)
    {
        print("Doing dem fade in");
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
        print("Done with the fadein");
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
        print("Done with the fadeout");
        player.SetActive(true);
        sprite.color = tmpColor;
    }

    private void DoFadeOut()
    {
        zillaBrain.resetZilla();
        // TODO: Get the player's start position from the state obj
        player.transform.SetLocalPositionAndRotation(
            new Vector3(7.02f, -1.36f, 0f), Quaternion.identity);
        // TODO: Get the current floor's object from the state thing
        GameObject.Find("Floor0").GetComponent<FloorController>().ResetPositions();
        print("Calling dat fadeout");
        StartCoroutine(fadeOut(GetComponent<SpriteRenderer>()));
    }

    public void Death()
    {
        player.SetActive(false);
        StartCoroutine(fadeIn(GetComponent<SpriteRenderer>()));
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDSidepanelController : MonoBehaviour
{
    public SpriteRenderer StepOnPplRenderer;

    public void ShowItem(string ItemTag)
    {
        if (ItemTag == "StepOnPpl")
        {
            StepOnPplRenderer.enabled = true;
        }
    }
}

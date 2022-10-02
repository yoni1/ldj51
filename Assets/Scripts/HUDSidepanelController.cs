using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDSidepanelController : MonoBehaviour
{
    public SpriteRenderer TempItemRenderer;

    public void ShowItem(string ItemTag)
    {
        if (ItemTag == "Temp_Item")
        {
            TempItemRenderer.enabled = true;
        }
    }
}

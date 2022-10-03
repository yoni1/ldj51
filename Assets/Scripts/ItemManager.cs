using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public HUDSidepanelController sidepanel;
    public bool hasStepOnPpl;

    public void OnPickedUp(string ItemTag)
    {
        sidepanel.ShowItem(ItemTag);

        if (ItemTag == "StepOnPpl")
        {
            hasStepOnPpl = true;
        }
    }
}

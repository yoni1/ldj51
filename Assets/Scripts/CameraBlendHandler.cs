using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class CameraBlendHandler : MonoBehaviour
{
    public GameObject player;
    public ZillaBrain zillaBrain;

    private CinemachineBrain brain;
    private bool currentlyBlending;

    // Start is called before the first frame update
    void Start()
    {
        brain = GetComponent<CinemachineBrain>();
        currentlyBlending = false;
    }

    void SuccessMoveToNextFloor(){
        currentlyBlending = false;
        player.SetActive(true);
        zillaBrain.resetZilla(true);
        zillaBrain.GetFloorController().VoiceAnnounce(); // Only announces on the first load
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!currentlyBlending && brain.IsBlending)
        {
            currentlyBlending = true;
            player.SetActive(false);
        }
        else if (currentlyBlending && !brain.IsBlending)
        {
            SuccessMoveToNextFloor();
        }
    }
}
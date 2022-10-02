using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class CameraBlendHandler : MonoBehaviour
{
    public GameObject player;

    private CinemachineBrain brain;
    private bool currentlyBlending;

    // Start is called before the first frame update
    void Start()
    {
        brain = GetComponent<CinemachineBrain>();
        currentlyBlending = false;
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
            currentlyBlending = false;
            player.SetActive(true);
        }
    }
}
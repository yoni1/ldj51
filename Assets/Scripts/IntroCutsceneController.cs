using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutsceneController : MonoBehaviour
{
    public AudioSource dialog;
    
    void Update()
    {
        if (Input.GetKeyDown("space") || !dialog.isPlaying)
        {
            SceneManager.LoadScene("FirstLayerScene");
        }
    }
}

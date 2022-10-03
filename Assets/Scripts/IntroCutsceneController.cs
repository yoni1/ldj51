using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroCutsceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VideoPlayer player = GetComponent<VideoPlayer>();
        player.url = System.IO.Path.Combine(Application.streamingAssetsPath, "OpeningCutscene.mp4");
        player.loopPointReached += EndReached;
        player.Play();
    }

    void NextScene()
    {
        SceneManager.LoadScene("Building");
    }

    void EndReached(VideoPlayer vp)
    {
        NextScene();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextScene();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Finish : MonoBehaviour
{

    public VideoPlayer videoPlayer;
    public Canvas canvas;

    private bool reached = false;

    private void Update()
    {
        if (reached)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                StatsHolder.Reset();
                SceneManager.LoadScene("SampleScene");
            }
            if (videoPlayer.isPaused)
            {
                StatsHolder.Reset();
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        reached = true;
        videoPlayer.Play();
        canvas.enabled = false;
    }

}

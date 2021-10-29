using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    private Player player;

    private float touchCheckpoint;

    private int numTouches = 0;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (numTouches == 0)
            {
                touchCheckpoint = Time.time + StatsHolder.TouchWindow;
            }
            numTouches++;
        }

        if (numTouches > 0)
        {
            if (Time.time > touchCheckpoint)
            {
                if (numTouches > 1)
                {
                    player.InvertGravity();
                }   
                else
                {
                    if (player.IsGrounded)
                    {
                        player.Jump();
                    }
                }
                numTouches = 0;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    private Player player;
    private float touchWindow = 0.5f;
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
            if (Time.time < touchCheckpoint)
            {
                player.InvertGravity();
                touchCheckpoint = float.PositiveInfinity;
            }
            else
            {
                if (player.IsGrounded)
                {
                    player.Jump();
                }
            }
            touchCheckpoint = Time.time + touchWindow;
        }
    }
}

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
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Ended)
        {
            if (touch.deltaPosition.magnitude < 0.125f)
            {
                if (player.IsGrounded)
                {
                    player.Jump();
                }
            }
            else
            {
                player.InvertGravity();
            }
        }
    }
}

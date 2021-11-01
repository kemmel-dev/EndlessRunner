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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.InvertGravity();

        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (player.IsGrounded)
            {
                player.Jump();
            }
        }
    }
}

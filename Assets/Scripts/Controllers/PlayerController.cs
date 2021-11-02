using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    private Player player;

    private float touchCheckpoint;

    private int numTouches = 0;

    public bool DEV_MODE;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            HandleInputDEV();
            return;
        }
        HandleInput();
    }

    private void HandleInputDEV()
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.InvertGravity();
        }
    }

    private void HandleInput()
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Ended)
        {
            if (touch.deltaPosition.magnitude < 0.125f)
            {
                return;
            }
            player.InvertGravity();
        }
    }
}

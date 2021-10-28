using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : Motor
{
    private float jumpCheckpoint;


    [field: SerializeField]
    private float jumpDuration = 1f;

    [field: SerializeField]
    private float jumpStrength;
    private float _jumpStrength;

    public float JumpStrength
    {
        get
        {
            if (Time.time > jumpCheckpoint)
            {
                return 0;
            }
            float percentageDuration = 1 - (jumpCheckpoint - Time.time) / jumpDuration;
            return Mathf.Lerp(_jumpStrength, -_jumpStrength, percentageDuration);
        }
        set
        {
            jumpCheckpoint = Time.time + jumpDuration;
            _jumpStrength = value;
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void Propel()
    {
        base.Propel();
    }

    public override void Propel(Vector3 speed)
    {
        base.Propel(speed);
    }

    public override void Brake()
    {
        base.Brake();
    }



    internal void Jump()
    {
        JumpStrength = jumpStrength;
    }

    public void StopJump()
    {
        jumpCheckpoint = float.NegativeInfinity;
    }
}

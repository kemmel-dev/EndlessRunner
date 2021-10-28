using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMotor : Motor
{


    private float initialY;
    private float swayAngle = 0;

    [field: SerializeField]
    private float swayPeriod = 0.02f;


    [field: SerializeField]
    private float swayAmplitude;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        initialY = transform.position.y;
    }

    private void FixedUpdate()
    {
        Propel();
    }

    public override void Propel()
    {
        rigidBody.MovePosition(new Vector3(transform.position.x, initialY + swayAmplitude * Mathf.Sin(swayAngle += swayPeriod * Time.deltaTime), transform.position.z));
    }

    public override void Brake()
    {
        base.Brake();
    }
}

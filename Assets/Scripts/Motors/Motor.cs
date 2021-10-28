using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public abstract class Motor : MonoBehaviour
{

    public virtual Vector3 Speed { get; set; }

    [field: SerializeField]
    protected float brakeFactor;

    [field: SerializeField]
    protected float minVelocity;

    [field: SerializeField]
    protected Transform target;

    protected Rigidbody2D rigidBody;

    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        target = this.transform;
    }

    public virtual void Propel()
    {
        rigidBody.MovePosition(target.position + Speed);
    }

    public virtual void Propel(Vector3 speed)
    {
        rigidBody.MovePosition(target.position + speed);
    }

    public virtual void Brake()
    {
        if (rigidBody.velocity.magnitude < minVelocity)
        {
            rigidBody.velocity = Vector2.zero;
            return;
        }
        rigidBody.velocity *= 1 - brakeFactor;
    }

}

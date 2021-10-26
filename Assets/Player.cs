using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    public SideScrollingCamera sideScrollingCamera;

    public float SpeedX
    {
        get
        {
            if (sideScrollingCamera.IsAheadOfPlayer)
            {
                return normalSpeed * catchUpFactor;
            }
            return normalSpeed;
        }
    }

    public float normalSpeed;
    public float catchUpFactor;

    public float gravityValue;

    public bool invertGravity;

    private Transform graphicsHolder;
    private Transform physicsHolder;

    private float boxColliderHeight;

    private BoxCollider2D feet;

    private bool grounded;

    private float Gravity
    {
        get
        {
            return invertGravity ? gravityValue : -gravityValue;
        }
    }

    private void Start()
    {
        graphicsHolder = transform.Find("GFX");
        physicsHolder = transform.Find("Physics");
        rigidBody = GetComponent<Rigidbody2D>();
        boxColliderHeight = physicsHolder.transform.Find("Body").GetComponent<BoxCollider2D>().size.y;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvertGravity();
        }

        if (!sideScrollingCamera.HasViewOfPlayer)
        {
            Destroy(this.gameObject);
        }
    }

    private void InvertGravity()
    {
        invertGravity = !invertGravity;

        int signOffset = invertGravity ? 1 : -1;

        transform.position = new Vector3(transform.position.x, transform.position.y + signOffset * boxColliderHeight, transform.position.z);
        graphicsHolder.localScale = new Vector3(graphicsHolder.localScale.x, -graphicsHolder.localScale.y, graphicsHolder.localScale.z);
        physicsHolder.localScale = new Vector3(physicsHolder.localScale.x, -physicsHolder.localScale.y, physicsHolder.localScale.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!grounded)
        {
            rigidBody.MovePosition(transform.position + new Vector3(SpeedX * Time.deltaTime, Gravity * Time.deltaTime));
            return;
        }
        rigidBody.MovePosition(transform.position + new Vector3(SpeedX * Time.deltaTime, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        grounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        grounded = false;
    }
}

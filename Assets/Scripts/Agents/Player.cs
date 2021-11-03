using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMotor))]

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{

    public SideScrollingCamera sideScrollingCamera;


    [field: SerializeField]
    private SpriteRenderer headRenderer;

    [field: SerializeField]
    private SpriteRenderer bodyRenderer;


    [field: SerializeField]
    private Color playerColorNormal;
    [field: SerializeField]
    private Color playerColorCatchingUp;

    public float SpeedX
    {
        get
        {
            float speed;
            if (sideScrollingCamera.IsAheadOfPlayer)
            {
                headRenderer.color = playerColorCatchingUp;
                speed = normalSpeed * catchUpFactor;
            }
            else
            {
                headRenderer.color = playerColorNormal;
                speed = normalSpeed;
            }
            return speed;
        }
    }

    internal void Jump()
    {
        playerMotor.Jump();
    }

    public float normalSpeed;
    public float catchUpFactor;


    [field: SerializeField]
    private float gravityValue;

    private bool invertGravity;

    private Transform graphicsHolder;
    private Transform physicsHolder;

    private float boxColliderHeight;
    private BoxCollider2D feet;

    private PlayerMotor playerMotor;

    private bool _isGrounded;

    private bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }

        set
        {
            _isGrounded = value;
        }
    }



    private float Gravity
    {
        get
        {
            float returnValue = gravityValue;
            return invertGravity ? returnValue : - returnValue;
        }
    }

    private void Start()
    {
        graphicsHolder = transform.Find("GFX");
        physicsHolder = transform.Find("Physics");
        boxColliderHeight = physicsHolder.transform.Find("Body").GetComponent<BoxCollider2D>().size.y;
        feet = physicsHolder.transform.Find("FeetTrigger").GetComponent<BoxCollider2D>();
        playerMotor = GetComponent<PlayerMotor>();

    }

    private void Update()
    {
        StatsHolder.Distance = (int) this.transform.position.x;
        if (!sideScrollingCamera.HasViewOfPlayer)
        {
            StatsHolder.Reset();
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void InvertGravity()
    {
        invertGravity = !invertGravity;

        int signOffset = invertGravity ? 1 : -1;

        //transform.position = new Vector3(transform.position.x, transform.position.y + signOffset * boxColliderHeight, transform.position.z);
        graphicsHolder.localScale = new Vector3(graphicsHolder.localScale.x, -graphicsHolder.localScale.y, graphicsHolder.localScale.z);
        physicsHolder.localScale = new Vector3(physicsHolder.localScale.x, -physicsHolder.localScale.y, physicsHolder.localScale.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsGrounded)
        {
            if (Gravity > 0)
            {
                playerMotor.Propel(new Vector2(SpeedX * Time.deltaTime, Gravity * Time.deltaTime));
                return;
            }
            playerMotor.Propel(new Vector2(SpeedX * Time.deltaTime, Gravity * Time.deltaTime));
            return;
        }
        if (Gravity > 0)
        {
            playerMotor.Propel(new Vector2(SpeedX * Time.deltaTime,  0));
            return;
        }
        playerMotor.Propel(new Vector2(SpeedX * Time.deltaTime,  0));
        return;
    }

    public void OnFeetTriggerEnter(OnTriggerDelegation delegation)
    {
        if (delegation.Other.CompareTag("Enemy"))
        {
            delegation.Other.GetComponentInParent<Enemy>().Kill();
            playerMotor.Jump();
            return;
        }
        if (delegation.Other.CompareTag("Floor"))
        {
            transform.position = delegation.Other.bounds.ClosestPoint(transform.position);
            // Player transform is at center of player. 
            // Here we calculate the distance from that center by using 
            // half the height of the bodyRenderer and multiplying that by the player scale.
            Vector3 distanceToPlayerCenter = new Vector3(0, bodyRenderer.transform.localScale.y / 2 * transform.localScale.y);
            transform.position += invertGravity ? -distanceToPlayerCenter : distanceToPlayerCenter;

            IsGrounded = true;
            playerMotor.StopJump();
        }
    }
    
    public void OnFeetTriggerExit(OnTriggerDelegation delegation)
    {
        if (delegation.Other.CompareTag("Floor"))
        {
            IsGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            StatsHolder.Reset();
            SceneManager.LoadScene("SampleScene");
            return;
        }
    }
}

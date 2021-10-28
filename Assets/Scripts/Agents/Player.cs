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

    internal void Jump()
    {
        playerMotor.Jump();
    }

    public float normalSpeed;
    public float catchUpFactor;


    [field: SerializeField]
    private float gravityValue;

    public bool invertGravity;

    private Transform graphicsHolder;
    private Transform physicsHolder;

    private float boxColliderHeight;
    private BoxCollider2D feet;

    public bool IsGrounded { get; private set; }

    private PlayerMotor playerMotor;

    private float Gravity
    {
        get
        {
            float returnValue = gravityValue + playerMotor.JumpStrength;
            return invertGravity ? returnValue : - returnValue;
        }
    }

    private void Start()
    {
        IsGrounded = true;
        graphicsHolder = transform.Find("GFX");
        physicsHolder = transform.Find("Physics");
        boxColliderHeight = physicsHolder.transform.Find("Body").GetComponent<BoxCollider2D>().size.y;
        feet = physicsHolder.transform.Find("FeetTrigger").GetComponent<BoxCollider2D>();
        playerMotor = GetComponent<PlayerMotor>();

    }

    private void Update()
    {
        if (!sideScrollingCamera.HasViewOfPlayer)
        {
            Destroy(this.gameObject);
        }
    }

    public void InvertGravity()
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
        if (!IsGrounded)
        {
            if (Gravity > 0)
            {
                playerMotor.Propel(new Vector2(SpeedX * Time.deltaTime, Gravity * Time.deltaTime - playerMotor.JumpStrength));
                return;
            }
            playerMotor.Propel(new Vector2(SpeedX * Time.deltaTime, Gravity * Time.deltaTime + playerMotor.JumpStrength));
            return;
        }
        if (Gravity > 0)
        {
            playerMotor.Propel(new Vector2(SpeedX * Time.deltaTime,  -playerMotor.JumpStrength));
            return;
        }
        playerMotor.Propel(new Vector2(SpeedX * Time.deltaTime,  playerMotor.JumpStrength));
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
        IsGrounded = true;
        playerMotor.StopJump();
    }
    
    public void OnFeetTriggerExit(OnTriggerDelegation delegation)
    {
        IsGrounded = false;
    }

    public void OnBodyTriggerEnter(OnTriggerDelegation delegation)
    {
        if (delegation.Other.CompareTag("Enemy"))
        {
            SceneManager.LoadScene("SampleScene");
            return;
        }
    }
}

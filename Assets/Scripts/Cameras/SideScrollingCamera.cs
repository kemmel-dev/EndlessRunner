using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollingCamera : MonoBehaviour
{

    public Player player;
    public float offsetX;
    public float outOfBoundsDistance;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(player.transform.position.x + offsetX, transform.position.y, transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x - outOfBoundsDistance, transform.position.y, transform.position.z));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        transform.position = new Vector3(transform.position.x + player.normalSpeed * Time.deltaTime, transform.position.y, transform.position.z);
    }

    public bool IsAheadOfPlayer
    {
        get
        {
            return player.transform.position.x < transform.position.x - offsetX;
        }
    }

    public bool HasViewOfPlayer
    {
        get
        {
            return player.transform.position.x >= transform.position.x - outOfBoundsDistance;
        }
    }
}

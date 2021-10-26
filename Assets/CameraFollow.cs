using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    public float offsetX;


    // Start is called before the first frame update
    void Start()
    {
        Follow();
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    public void Follow()
    {

        transform.position = new Vector3(target.position.x + offsetX, transform.position.y, transform.position.z);
    }
}

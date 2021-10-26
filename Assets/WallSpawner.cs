using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : ObjectSpawner
{

    public float SpawnDistance { get; set; }

    public override void Start()
    {
        base.Start();
    }

    public override void SpawnObjectAt(Vector3 position, Quaternion quaternion)
    {
        base.SpawnObjectAt(position, quaternion);
    }

    public override void SpawnObjectAt(Vector3 position)
    {
        base.SpawnObjectAt(position);
    }

    public override void SpawnObjectAt(Quaternion quaternion)
    {
        base.SpawnObjectAt(quaternion);
    }

    public void SpawnRandomWall()
    {
        SpawnObjectAt(new Vector3(transform.position.x + SpawnDistance, transform.position.y, transform.position.z));
    }
}

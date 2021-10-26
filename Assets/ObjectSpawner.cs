using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSpawner : MonoBehaviour
{
    private GameObject _objectPrefab;

    public GameObject objectPrefab;

    public virtual void Start()
    {
        SetPrefab(objectPrefab);
    }

    public void SetPrefab(GameObject gameObject)
    {
        _objectPrefab = gameObject;
    }

    public virtual void SpawnObjectAt(Vector3 position, Quaternion quaternion)
    {
        Instantiate(_objectPrefab, position, quaternion);
    }

    public virtual void SpawnObjectAt(Vector3 position)
    {
        Instantiate(_objectPrefab, position, Quaternion.identity);
    }

    public virtual void SpawnObjectAt(Quaternion quaternion)
    {
        Instantiate(_objectPrefab, Vector3.zero, quaternion);
    }
}
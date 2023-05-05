using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obj;

    public void SpawnObject()
    {
        Instantiate(obj, transform.position, Quaternion.identity);
    }
}

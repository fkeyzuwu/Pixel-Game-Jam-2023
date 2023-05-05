using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OobTriggerEvent : MonoBehaviour
{
    [SerializeField] private string TagName;
    [SerializeField] private UnityEvent executeEvents;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(TagName))
        {
            Destroy(other.gameObject);
            executeEvents?.Invoke();
        }
    }
}

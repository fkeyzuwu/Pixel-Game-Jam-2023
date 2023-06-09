using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HybridUniverseObject : MonoBehaviour
{
    void Start()
    {
        UniverseSwitchManager.Instance.OnUniverseChangedCallback += ChangeObject;
        ChangeObject(UniverseSwitchManager.Instance.currentUniverse);
    }

    void OnDestroy()
    {
        UniverseSwitchManager.Instance.OnUniverseChangedCallback -= ChangeObject;
    }

    protected abstract void ChangeObject(Universe universe);
}

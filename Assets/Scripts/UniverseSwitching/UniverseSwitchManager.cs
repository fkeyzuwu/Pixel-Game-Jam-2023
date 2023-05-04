using System;
using UnityEngine;

public class UniverseSwitchManager : MonoBehaviour
{
    #region Singleton
    public static UniverseSwitchManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion
    
    public Universe currentUniverse;
    
    public delegate void OnUniverseChanged(Universe universe);
    public OnUniverseChanged OnUniverseChangedCallback;

    public void SwitchUniverse()
    {
        currentUniverse = currentUniverse == Universe.Purple ? Universe.Red : Universe.Purple;
        OnUniverseChangedCallback?.Invoke(currentUniverse);
    }

    public void SetUniverse(Universe universe)
    {
        currentUniverse = universe;
        OnUniverseChangedCallback?.Invoke(currentUniverse);
    }
}

[Serializable]
public enum Universe { None, Red, Purple }
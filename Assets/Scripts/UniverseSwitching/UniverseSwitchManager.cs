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
    
    private Universe _currentUniverse;
    
    public delegate void OnUniverseChanged(Universe universe);
    public OnUniverseChanged OnUniverseChangedCallback;
    
    private void Start()
    {
        _currentUniverse = Universe.None;
    }

    public void SwitchUniverse()
    {
        if (_currentUniverse == Universe.None) return;
        _currentUniverse = _currentUniverse == Universe.Purple ? Universe.Red : Universe.Purple;
        OnUniverseChangedCallback?.Invoke(_currentUniverse);
    }

    public void SetUniverse(Universe universe)
    {
        _currentUniverse = universe;
        OnUniverseChangedCallback?.Invoke(_currentUniverse);
    }

}

[Serializable]
public enum Universe { None, Red, Purple }
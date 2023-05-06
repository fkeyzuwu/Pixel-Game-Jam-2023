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
        if (currentUniverse == Universe.None) return ;
        currentUniverse = currentUniverse == Universe.Red ? Universe.Purple : Universe.Red;

        if (currentUniverse  == Universe.Red)
        {
            AudioManager.Instance.PlaySound("EnterRedUniverse");
        }
        else if(currentUniverse == Universe.Purple)
        {
            AudioManager.Instance.PlaySound("EnterPurpleUniverse");
        }

        OnUniverseChangedCallback?.Invoke(currentUniverse);
    }

    public void SetUniverse(Universe universe)
    {
        currentUniverse = universe;

        if (currentUniverse == Universe.Red)
        {
            AudioManager.Instance.PlaySound("EnterRedUniverse");
        }
        else if (currentUniverse == Universe.Purple)
        {
            AudioManager.Instance.PlaySound("EnterPurpleUniverse");
        }

        OnUniverseChangedCallback?.Invoke(currentUniverse);
    }
}

[Serializable]
public enum Universe { None, Red, Purple }
using System;
using UnityEngine;

public class GameLayersManager : MonoBehaviour
{
    #region Singleton
    public static GameLayersManager Instance { get; private set; }

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

    public LayerMask groundLayerMask;
    public LayerMask wallLayerMask;
    public LayerMask grabbableObjectsLayerMask;
    
}

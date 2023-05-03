using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBox : MonoBehaviour
{
    [SerializeField] private Vector2 redUniverseScale;
    [SerializeField] private Vector2 purpleUniverseScale;
    [SerializeField] private float tweenDuration;
    [SerializeField] private LeanTweenType easeType;
    void Start()
    {
        UniverseSwitchManager.Instance.OnUniverseChangedCallback += ChangeSize;
    }

    private void OnDestroy()
    {
        UniverseSwitchManager.Instance.OnUniverseChangedCallback += ChangeSize;
    }

    private void ChangeSize(Universe universe) 
    {
        if(universe == Universe.Red) 
        {
            LeanTween.scale(gameObject, redUniverseScale, tweenDuration).setEase(easeType);
        }
        else if(universe == Universe.Purple) 
        {
            LeanTween.scale(gameObject, purpleUniverseScale, tweenDuration).setEase(easeType);
        }
    }
}

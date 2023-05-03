using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingObject : HybridUniverseObject
{
    [SerializeField] private Vector2 redUniverseScale;
    [SerializeField] private Vector2 purpleUniverseScale;
    [SerializeField] private float tweenDuration;
    [SerializeField] private LeanTweenType easeType;
    protected override void ChangeObject(Universe universe)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiStageScalingObject : HybridUniverseObject
{
    [SerializeField] private Vector2[] stages;
    [SerializeField] private float tweenDuration;
    [SerializeField] private LeanTweenType easeType;

    private int count = 0;
    protected override void ChangeObject(Universe universe)
    {
        LeanTween.scale(gameObject, stages[count], tweenDuration).setEase(easeType);
        if(count + 1 == stages.Length)
        {
            count = 0;
        }
        else
        {
            count++;
        }
    }
}

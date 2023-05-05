using System;
using System.Collections;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    #region Singleton
    public static HintManager Instance { get; private set; }

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
    
    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private Image hintBorder;

    private Canvas _hintCanvas;

    private void Start()
    {
        _hintCanvas = GetComponent<Canvas>();
        _hintCanvas.enabled = false;
        hintText.text = "";
        hintBorder.transform.localScale = Vector3.zero;
    }

    public void ShowHint(HintData hintData)
    {
        StartCoroutine(PlayHint(hintData));
    }
    
    private IEnumerator PlayHint(HintData hintData)
    {
        EnableHint(hintData.text);
        yield return new WaitForSeconds(hintData.durationInSeconds);
        DisableHint();
    }

    private void EnableHint(String hint)
    {
        hintText.text = hint;
        _hintCanvas.enabled = true;
        LeanTween.scale(hintBorder.rectTransform, new Vector3(1, 1, 1), 1f).setEase(LeanTweenType.easeOutBounce);
    }

    private void DisableHint()
    {
        LeanTween.scale(hintBorder.rectTransform, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInBounce)
            .setOnComplete(() =>
            {
                _hintCanvas.enabled = false;
                hintText.text = "";
            });
    }
}
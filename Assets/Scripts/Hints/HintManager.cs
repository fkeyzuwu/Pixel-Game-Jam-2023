using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    public HintData movementHint;
    
    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private Image hintBorder;

    private Canvas _hintCanvas;

    private void Awake()
    {
        _hintCanvas = GetComponent<Canvas>();
        _hintCanvas.enabled = false;
        hintText.text = "";
        /*hintBorder.transform.localScale = Vector3.zero; TODO: Uncomment when tween*/
    }

    IEnumerator ShowHint(HintData hintData)
    {
        EnableHint(hintData.text);
        yield return new WaitForSeconds(hintData.durationInSeconds);
        DisableHint();
    }

    private void EnableHint(String hint)
    {
        hintText.text = hint;
        _hintCanvas.enabled = true;
        // TODO: Add LeanTweet with DOScale(new Vector3(1, 1, 1), 1f).SetEase(Ease.OutBounce);
    }

    private void DisableHint()
    {
        /* TODO: Add LeanTweet with DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBounce).OnComplete(() =>
        {
            _hintCanvas.enabled = false;
            hintText.text = "";
        });*/
        _hintCanvas.enabled = false;
        hintText.text = "";
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(ShowHint(movementHint));
        }
    }
}
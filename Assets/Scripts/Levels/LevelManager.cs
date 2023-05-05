using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject blackScreenCanvas;
    [SerializeField] private float fadeInTime = 2f;
    [SerializeField] private float fadeOutTime = 1f;
    [SerializeField] private UnityEvent executeAfterLevelStart;
    [SerializeField] private UnityEvent executeAfterLevelEnd;
    [SerializeField] private string nextLevelSceneName;

    private Canvas _canvas;
    private CanvasGroup _canvasGroup;
    
    private void Start()
    {
        _canvas = blackScreenCanvas.GetComponent<Canvas>();
        _canvasGroup = blackScreenCanvas.GetComponent<CanvasGroup>();
        _canvas.enabled = false;
        StartLevel();
    }

    private void StartLevel()
    {
        FadeIn(ExecuteAfterLevelStart);
    }

    public void EndLevel()
    {
        FadeOut(ExecuteAfterLevelEnd);
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadSceneAsync(nextLevelSceneName);
    }

    public void RestartLevel()
    {
        FadeOut(() =>
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        });
    }

    private void FadeIn(Action functionAfterFadeIn)
    {
        _canvas.enabled = true;
        _canvasGroup.alpha = 1;
        LeanTween.alphaCanvas(_canvasGroup, 0, fadeInTime).setOnComplete(functionAfterFadeIn);
    }

    private void ExecuteAfterLevelStart()
    {
        _canvas.enabled = false;
        executeAfterLevelStart?.Invoke();
    }

    private void FadeOut(Action functionAfterFadeOut)
    {
        _canvas.enabled = true;
        _canvasGroup.alpha = 0;
        LeanTween.alphaCanvas(_canvasGroup, 1, fadeOutTime).setOnComplete(functionAfterFadeOut);
    }

    private void ExecuteAfterLevelEnd()
    {
        executeAfterLevelEnd?.Invoke();
        GoToNextLevel();
    }
}

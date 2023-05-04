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
        FadeIn();
    }

    public void EndLevel()
    {
        FadeOut();
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadSceneAsync(nextLevelSceneName);
    }

    private void FadeIn()
    {
        _canvas.enabled = true;
        _canvasGroup.alpha = 1;
        LeanTween.alphaCanvas(_canvasGroup, 0, fadeInTime).setOnComplete(() =>
        {
            _canvas.enabled = false;
            executeAfterLevelStart?.Invoke();
        });
    }

    private void FadeOut()
    {
        _canvas.enabled = true;
        _canvasGroup.alpha = 0;
        LeanTween.alphaCanvas(_canvasGroup, 1, fadeOutTime).setOnComplete(() =>
        {
            executeAfterLevelEnd?.Invoke();
            GoToNextLevel();
        });
    }
}

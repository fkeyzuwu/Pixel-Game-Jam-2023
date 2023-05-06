using UnityEngine;
using UnityEngine.SceneManagement;

public class Prologue : MonoBehaviour
{
    [SerializeField] private Story story;
    
    void Start()
    {
        if (story != null)
            StoryTellerManager.Instance.StartStory(story);    
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
    
}

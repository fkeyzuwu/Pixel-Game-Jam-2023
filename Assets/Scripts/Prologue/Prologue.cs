using UnityEngine;

public class Prologue : MonoBehaviour
{
    [SerializeField] private Story story;
    
    void Start()
    {
        if (story != null)
            StoryTellerManager.Instance.StartStory(story);    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StoryTellerManager.Instance.DisplayNextSentence();
        }

    }
}

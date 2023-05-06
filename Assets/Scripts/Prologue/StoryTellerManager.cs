using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StoryTellerManager : MonoBehaviour
{
    #region Singleton

    public static StoryTellerManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            _inStoryMode = false;
            _storyLines = new Queue<StoryLine>();
            _sentences = new Queue<string>();
        }
    }

    #endregion

    private static readonly int IsOpen = Animator.StringToHash("IsOpen");

    [SerializeField] private Image backgroundImage;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private Animator animator;
    [SerializeField] private UnityEvent afterStoryFinishedActions;

    private bool _inStoryMode;
    private Queue<StoryLine> _storyLines;
    private Queue<string> _sentences;
    private UnityEvent _afterStoryActions;

    private void Update()
    {
        if (_inStoryMode && Input.GetKeyDown(KeyCode.E))
        {
            DisplayNextSentence();
        }

    }
    
    public void StartStory(Story story)
    {
        _inStoryMode = true;
        _storyLines.Clear();
        _sentences.Clear();
        foreach (var storyLine in story.StoryLines)
        {
            _storyLines.Enqueue(storyLine);
        }

        DisplayNextStoryLine();
    }

    private void DisplayNextStoryLine()
    {
        text.text = "";
        StopAllCoroutines();
        if (_storyLines.Count == 0)
        {
            EndStory();
            return;
        }

        StoryLine storyLine = _storyLines.Dequeue();
        backgroundImage.sprite = storyLine.backgroundImage;
        LeanTween.value(backgroundImage.gameObject, SetSpriteAlpha, 0f, 1f, 1f).setOnComplete(() =>
        {
            animator.SetBool(IsOpen, true);
            foreach (var sentence in storyLine.sentences)
            {
                _sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        });
    }

    public void DisplayNextSentence()
    {
        text.text = "";
        StopAllCoroutines();
        if (_sentences.Count == 0)
        {
            DisplayNextStoryLine();
            return;
        }
        
        string sentence = _sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        text.text = "";
        foreach (char letter in sentence)
        {
            text.text += letter;
            yield return new WaitForSeconds(0.04f);
        }
    }

    private void EndStory()
    {
        StopAllCoroutines();
        text.text = "";
        hintText.text = "";
        _inStoryMode = false;
        LeanTween.value(backgroundImage.gameObject, SetSpriteAlpha, 1f, 0f, 1f).setOnComplete(() =>
        {
            afterStoryFinishedActions?.Invoke();
        });
    }
    
    private void SetSpriteAlpha(float val)
    {
        backgroundImage.color = new Color(1f, 1f, 1f, val);
    }
}
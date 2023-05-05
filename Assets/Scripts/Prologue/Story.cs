using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Story/New Story")]
public class Story : ScriptableObject
{
    public StoryLine[] StoryLines;
}
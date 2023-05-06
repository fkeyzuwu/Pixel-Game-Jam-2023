using UnityEngine;

[CreateAssetMenu(menuName = "Story/New Story Line")]
public class StoryLine : ScriptableObject
{
    [TextArea(5, 10)] public string[] sentences;
    public Sprite backgroundImage;
}

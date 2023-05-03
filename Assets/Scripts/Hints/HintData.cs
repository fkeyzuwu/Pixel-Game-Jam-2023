using UnityEngine;

[CreateAssetMenu(menuName = "Hint/New Hint", fileName = "New Hint")]
public class HintData : ScriptableObject
{
    public string text;
    public float durationInSeconds;
}

using UnityEngine;

public class HintHolder : MonoBehaviour
{
    [SerializeField] private HintData hintData;

    public void ShowHint()
    {
        StartCoroutine(HintManager.Instance.ShowHint(hintData));
    }
}

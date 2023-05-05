using UnityEngine;

public class HintHolder : MonoBehaviour
{
    [SerializeField] private HintData hintData;

    public void ShowHint()
    {
        HintManager.Instance.ShowHint(hintData);
    }
}

using UnityEngine;

public class ActiveGameObjectsUtil : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;

    public void ActiveGameObjects()
    {
        foreach (var go in gameObjects)
        {
            go.SetActive(true);
        }

        AudioManager.Instance.PlaySound("WhiteNoise");
    }
}

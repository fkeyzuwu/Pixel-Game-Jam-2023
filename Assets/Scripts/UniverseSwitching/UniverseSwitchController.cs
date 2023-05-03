using System;
using UnityEngine;

public class UniverseSwitchController : MonoBehaviour
{
    [SerializeField] public GameObject redUniverseGameObject;
    [SerializeField] public GameObject purpleUniverseGameObject;

    private void Start()
    {
        UniverseSwitchManager.Instance.OnUniverseChangedCallback += UpdateUniverseGameObjects;
    }

    private void OnDestroy()
    {
        UniverseSwitchManager.Instance.OnUniverseChangedCallback -= UpdateUniverseGameObjects;
    }

    private void UpdateUniverseGameObjects(Universe universe)
    {
        if (universe == Universe.Red)
        {
            redUniverseGameObject.SetActive(true);
            purpleUniverseGameObject.SetActive(false);
        }
        else if (universe == Universe.Purple)
        {
            purpleUniverseGameObject.SetActive(true);
            redUniverseGameObject.SetActive(false);
        }
    }
}

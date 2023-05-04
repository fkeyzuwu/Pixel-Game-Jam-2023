using System;
using UnityEngine;

public class UniverseSwitchController : MonoBehaviour
{
    [SerializeField] public GameObject redUniverseStaticObjects;
    [SerializeField] public GameObject purpleUniverseStaticObjects;

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
            redUniverseStaticObjects.SetActive(true);
            purpleUniverseStaticObjects.SetActive(false);
        }
        else if (universe == Universe.Purple)
        {
            redUniverseStaticObjects.SetActive(false);
            purpleUniverseStaticObjects.SetActive(true);
        }
    }
}

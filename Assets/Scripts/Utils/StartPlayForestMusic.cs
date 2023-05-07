using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlayForestMusic : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayMainMusic();
    }
}

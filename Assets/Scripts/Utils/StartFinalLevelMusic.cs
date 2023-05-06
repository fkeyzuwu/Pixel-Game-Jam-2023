using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFinalLevelMusic : MonoBehaviour
{
    public void Play()
    {
        AudioManager.Instance.PlayFinalMusic();
    }
}

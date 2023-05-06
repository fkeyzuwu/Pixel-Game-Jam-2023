using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    private bool isGamePaused = false;
    private bool isInOptionsMenu = false;
    [SerializeField] private GameObject pauseMenuUi;
    [SerializeField] private GameObject optionsMenuUi;

    private void Awake()
    {
        Instance = this;
    }

    public void Toggle()
    {
        if (isInOptionsMenu)
        {
            CloseOptionsMenu();
            return;
        }

        isGamePaused = !isGamePaused;
        pauseMenuUi.SetActive(isGamePaused);
        Time.timeScale = isGamePaused ? 0f : 1f;
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        GameObject.Find("LevelManager").GetComponent<LevelManager>().RestartLevel();
        pauseMenuUi.SetActive(false);
    }

    public void OpenOptionsMenu()
    {
        pauseMenuUi.SetActive(false);
        optionsMenuUi.SetActive(true);
        isInOptionsMenu = true;
    }

    public void CloseOptionsMenu()
    {
        pauseMenuUi.SetActive(true);
        optionsMenuUi.SetActive(false);
        isInOptionsMenu = false;
    }

    public void ChangeMasterVolume(float volume)
    {
        AudioManager.Instance.masterGroup.audioMixer.SetFloat("MasterVolume", volume);
    }

    public void ChangeMusicVolume(float volume)
    {
        AudioManager.Instance.masterGroup.audioMixer.SetFloat("MusicVolume", volume);
    }

    public void ChangeSfxVolume(float volume)
    {
        AudioManager.Instance.masterGroup.audioMixer.SetFloat("SfxVolume", volume);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}

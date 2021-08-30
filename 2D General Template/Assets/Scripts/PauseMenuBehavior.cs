/*****************************************************************************
// File Name :         PauseMenuBehavior.cs
// Author :            Jacob Welch
// Creation Date :     28 August 2021
//
// Brief Description : Handles the pause menu and allows players to pause the game.
*****************************************************************************/
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class PauseMenuBehavior : MenuBehavior
{
    /// <summary>
    /// Holds true if the game is currently paused.
    /// </summary>
    private bool isPaused = false;

    /// <summary>
    /// All of the audio sources in the scene.
    /// </summary>
    private List<AudioSource> allAudioSources;

    /// <summary>
    /// The AudioSource of this levels music.
    /// </summary>
    private AudioSource levelMusic;

    [SerializeField]
    [Tooltip("The pause menu gameobject")]
    private GameObject pauseMenu = null;

    /// <summary>
    /// Initializes components.
    /// </summary>
    private void Awake()
    {
        levelMusic = GameObject.Find("Level Music").GetComponent<AudioSource>();

        allAudioSources = FindObjectsOfType<AudioSource>().ToList();

        allAudioSources.Remove(levelMusic);
    }

    /// <summary>
    /// If the player hits the pause game key, the game is paused.
    /// </summary>
    public void OnPauseGame()
    {
        pauseMenu.SetActive(!isPaused);

        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }

        isPaused = !isPaused;
    }

    /// <summary>
    /// Resumes the game and its audiosources.
    /// </summary>
    private void ResumeGame()
    {
        // UnPauses all audio sources in the scene
        foreach (AudioSource aud in allAudioSources)
        {
            if (aud != null)
            {
                aud.UnPause();
            }
        }

        Time.timeScale = 1;
    }

    /// <summary>
    /// Pauses the game and its audiosources.
    /// </summary>
    private void PauseGame()
    {
        // Pauses all audio sources in the scene
        foreach (AudioSource aud in allAudioSources)
        {
            if (aud != null)
            {
                aud.Pause();
            }
        }

        Time.timeScale = 0;
    }

    /// <summary>
    /// Restarts the current level from the beginning.
    /// </summary>
    public void RestartLevel()
    {
        Time.timeScale = 1;

        if (!hasLoadScreen)
        {
            StartCoroutine(LoadSceneHelper(SceneManager.GetActiveScene().name));
        }
    }
}

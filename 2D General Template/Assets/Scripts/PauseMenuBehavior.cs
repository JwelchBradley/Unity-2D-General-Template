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
using System;
using System.Collections;

public class PauseMenuBehavior : MenuBehavior
{
    #region Variables
    /// <summary>
    /// Holds true if the game is currently paused.
    /// </summary>
    private bool isPaused = false;

    /// <summary>
    /// Enables and disables the pause feature.
    /// </summary>
    private bool canPause = false;

    [SerializeField]
    [Tooltip("The pause menu gameobject")]
    private GameObject pauseMenu = null;
    #endregion

    #region Functions
    /// <summary>
    /// Initializes components.
    /// </summary>
    private void Awake()
    {
        StartCoroutine(WaitFadeIn());
    }

    private IEnumerator WaitFadeIn()
    {
        yield return new WaitForSeconds(crossfadeAnim.GetCurrentAnimatorStateInfo(0).length);

        canPause = true;
    }

    /// <summary>
    /// If the player hits the pause game key, the game is paused.
    /// </summary>
    public void OnPauseGame()
    {
        // Opens pause menu and pauses the game
        if (canPause)
        {
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);
            AudioListener.pause = isPaused;
            Time.timeScale = Convert.ToInt32(!isPaused);
        }
    }

    /// <summary>
    /// Restarts the current level from the beginning.
    /// </summary>
    public void RestartLevel()
    {
        canPause = false;

        if (!hasLoadScreen)
        {
            StartCoroutine(LoadSceneHelper(SceneManager.GetActiveScene().name));
        }
    }
    #endregion
}
/*****************************************************************************
// File Name :         MenuBehavior.cs
// Author :            Jacob Welch
// Creation Date :     28 August 2021
//
// Brief Description : Handles the chaning of panels and scenes from menus.
*****************************************************************************/
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
    #region Variables
    [SerializeField]
    [Tooltip("Animator of level crossfade")]
    protected Animator crossfadeAnim;

    [SerializeField]
    [Tooltip("Tick true if there is a loading screen")]
    protected bool hasLoadScreen;
    #endregion

    #region Functions
    /// <summary>
    /// Sets a panel to be active.
    /// </summary>
    /// <param name="panel">The panel to be set active.</param>
    public void LoadPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    /// <summary>
    /// Sets a panel to be inactive.
    /// </summary>
    /// <param name="panel">The panel to be set inactive.</param>
    public void DisablePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    /// <summary>
    /// Loads a scene from a given string.
    /// </summary>
    /// <param name="sceneName">Scene to be loaded.</param>
    public void LoadScene(string sceneName)
    {
        if (!hasLoadScreen)
        {
            StartCoroutine(LoadSceneHelper(sceneName));
        }
    }

    /// <summary>
    /// Loads a scene after the level has faded out.
    /// </summary>
    /// <param name="sceneName">Scene to be loaded.</param>
    /// <returns></returns>
    protected IEnumerator LoadSceneHelper(string sceneName)
    {
        crossfadeAnim.SetBool("levelEnd", true);

        yield return null;

        // Preloads the scene and then loads it after the scene has faded out
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneName);
        loadOp.allowSceneActivation = false;

        yield return new WaitForSecondsRealtime(crossfadeAnim.GetCurrentAnimatorStateInfo(0).length);

        Time.timeScale = 1;

        loadOp.allowSceneActivation = true;
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
    #endregion
}
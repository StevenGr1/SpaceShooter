using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameOver : MonoBehaviour
{
    public GameObject GameOverUI;

    void Update()
    {
        if (IHM.instance.currentLife <= 0)
        {
            GameOverUI.SetActive(true);
        }
    }

    public void Recommencer()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Jeu");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}

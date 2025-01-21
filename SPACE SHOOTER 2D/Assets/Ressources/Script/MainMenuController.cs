using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class MainMenuController : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Jeu");
    }

    public void Quit()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR             
        EditorApplication.isPlaying = false;
#endif
    }
}

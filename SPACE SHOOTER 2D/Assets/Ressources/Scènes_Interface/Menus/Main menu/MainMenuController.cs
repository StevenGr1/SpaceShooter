using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Diagnostics;


#if UNITY_EDITOR
using UnityEditor;
#endif


public class MainMenuController : MonoBehaviour
{
    public RawImage fadeImage;
    float fadeDuration = 4f;

    public void Awake()
    {
        fadeImage.gameObject.SetActive(true);
    }

    public void Start()
    {
        fadeImage.gameObject.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("Jeu");
    }

    public void Credits()
    {
        StartCoroutine(FadeAndLoadScene());
    }

    IEnumerator FadeAndLoadScene()
    {
        fadeImage.gameObject.SetActive(true);
        float t = 0f;
        Color originalColor = fadeImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(0f, 1f, t / fadeDuration));
            yield return null;
        }
        fadeImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
        SceneManager.LoadScene("Crédits");
    }

    ////////////////////////////////////////////////////////////////////////////////////

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

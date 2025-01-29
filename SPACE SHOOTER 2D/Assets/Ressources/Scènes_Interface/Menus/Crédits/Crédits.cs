using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Crédits : MonoBehaviour
{
    public RawImage fadeImage;
    float fadeDuration = 4f;
    public GameObject echap;

    void Start()
    {
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0f);
        StartCoroutine(FadeOutAndQuit());
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
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator FadeOutAndQuit()
    {
        fadeImage.gameObject.SetActive(true);
        float t = 0f;
        Color originalColor = fadeImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1f, 0f, t / fadeDuration));
            yield return null;
        }
        fadeImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        fadeImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            echap.gameObject.SetActive(true);
            StartCoroutine(FadeAndLoadScene());
        }
    }
}
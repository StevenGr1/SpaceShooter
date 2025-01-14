using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IHM : MonoBehaviour
{
    public static IHM instance;

    [SerializeField]
    TextMeshProUGUI lifeText;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void UpdatePlayerLife()
    {
        lifeText.text = PlayerControler.instance.currentLife.ToString();
    }
}
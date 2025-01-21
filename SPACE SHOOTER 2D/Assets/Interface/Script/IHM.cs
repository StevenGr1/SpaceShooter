using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IHM : MonoBehaviour
{
    public static IHM instance;

    [SerializeField]
    TextMeshProUGUI lifeText;
    [SerializeField]
    TextMeshProUGUI pointText;

    //////////////////////////////// CREATION D'INSTANCE //////////////////////////////////////////////////////////////////////////

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    //////////////////////////////// UPDATE DES TEXTES //////////////////////////////////////////////////////////////////////////

    public void UpdatePlayerLife()
    {
        lifeText.text = PlayerControler.instance.currentLife.ToString();
    }
    public void UpdatePlayerPoint()
    {
        pointText.text = PlayerControler.instance.currentPoint.ToString();
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IHM : MonoBehaviour, IDamageable
{
    public static IHM instance;

    [SerializeField] TextMeshProUGUI lifeText;
    [SerializeField] TextMeshProUGUI pointText;

    public int maxLife = 5;
    public int currentLife = 5;
    public int currentPoint = 0;

    public AudioSource audioSource;
    GameObject player;

    //////////////////////////////// CREATION D'INSTANCE //////////////////////////////////////////////////////////////////////////

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        currentLife = maxLife;
    }

    void Start()
    {
        player = GameObject.Find("Player");
        if (player != null)
        {
            audioSource = player.GetComponent<AudioSource>();
        }
    }

    //////////////////////////////// UPDATE DES TEXTES //////////////////////////////////////////////////////////////////////////

    public void UpdatePlayerLife()
    {
        lifeText.text = currentLife.ToString();
    }
    public void UpdatePlayerPoint()
    {
        pointText.text = IHM.instance.currentPoint.ToString();
    }

    //////////////////////////////// SYSTEME DE PERTE DE VIE + EXPLOSION  //////////////////////////////////////////////////////////////////////////

    public void SetDamage(int damage, IDamageable attacker)
    {
        currentLife -= damage;
        UpdatePlayerLife();

        if (currentLife <= 0)
        {
            currentLife = 0;
            SpriteRenderer spriteRenderer = player.GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }
            if (audioSource != null)
            {
                AudioClip clip = PlaylistB.instance.GetAudioClip(0);
                if (clip != null)
                {
                    audioSource.clip = clip;
                    audioSource.Play();
                }
            }
            StartCoroutine(Destroy());
        }
    IEnumerator Destroy()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            yield return new WaitForSeconds(audioSource.clip.length);
        }
        Time.timeScale = 0;
        }
    }

    //////////////////////////////// SYSTEME DE GAIN DE KILL //////////////////////////////////////////////////////////////////////////

    public void SetPoint(int point)
    {
        currentPoint += point;
        IHM.instance.UpdatePlayerPoint();
    }
}
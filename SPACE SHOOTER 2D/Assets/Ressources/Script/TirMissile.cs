using System.Collections;
using UnityEngine;

public class TirMissile : MonoBehaviour, IDamageable
{
    public float moveSpeed = 6f;
    public GameObject Launcher;
    float DespawnTime = 3f;

    public AudioSource audioSource;
    public GameObject player;

    //////////////////////////////// DESPAWN TIR //////////////////////////////////////////////////////////////////////////

    void Start()
    {
        StartCoroutine(Despawn());
        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(DespawnTime);
        Destroy(gameObject);
    }

    //////////////////////////////// MOUVEMENT + ETAT ENDOMMAGEABLE //////////////////////////////////////////////////////////////////////////

    void Update()
    {
        transform.Translate(0f, moveSpeed * Time.deltaTime, 0f);
    }

    public void SetDamage(int damage, IDamageable attacker)
    {
        throw new System.NotImplementedException();
    }

    //////////////////////////////// COLLISIONS + SONS //////////////////////////////////////////////////////////////////////////

    public void Depop()
    {
        audioSource.Play();
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Collider2D monCollider = GetComponent<Collider2D>();
        if (spriteRenderer != null && monCollider != null)
        {
            spriteRenderer.enabled = false;
            monCollider.enabled = false;
        }
        StartCoroutine(Boom());
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Launcher) return;

        if (collision.CompareTag("Player"))
        {
            if (PlaylistB.instance != null)
            {
                AudioClip clip = PlaylistB.instance.GetAudioClip(1);
                if (clip != null)
                {
                    if (audioSource != null)
                    {
                        audioSource.clip = clip;
                        IHM.instance.SetDamage(1, this);
                        Depop();
                    }
                }
            }
        }

        else if (collision.CompareTag("Projectile"))
        {
            TirMissile projectileCollision = collision.gameObject.GetComponent<TirMissile>();
            if (projectileCollision != null && projectileCollision.Launcher.CompareTag("Ennemi"))
            {
                if (PlaylistB.instance != null)
                {
                    AudioClip clip = PlaylistB.instance.GetAudioClip(1);
                    if (clip != null)
                    {
                        if (audioSource != null)
                        {
                            audioSource.clip = clip;
                            Depop();
                            Destroy(collision.gameObject);
                        }
                    }
                }
            }
        }

        else if (collision.CompareTag("Ennemi"))
        {
            if (!Launcher.CompareTag("Ennemi"))
            {
                if (PlaylistB.instance != null)
                {
                    AudioClip clip = PlaylistB.instance.GetAudioClip(0);
                    if (clip != null)
                    {
                        if (audioSource != null)
                        {
                            audioSource.clip = clip;
                            IHM.instance.SetPoint(1);
                            Destroy(collision.gameObject);
                            Depop();
                        }
                    }
                }
            }
        }

        else if (collision.CompareTag("Obstacle"))
        {
            if (Launcher.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }

    //////////////////////////////// PLAYLIST /////////////////////////////////////////////////////////////////////////////////////////

    public void PlaySoundFromPlaylist(int index)
    {
        GameObject playlistObject = GameObject.Find("PlaylistBruitages");

        if (playlistObject != null)
        {
            PlaylistB playlist = playlistObject.GetComponent<PlaylistB>();

            if (playlist != null)
            {
                AudioClip clip = playlist.GetAudioClip(index);
                if (clip != null)
                {
                    audioSource.clip = clip;
                    audioSource.Play();
                }
            }
            else
            {
                Debug.LogWarning("Script PlaylistB introuvable sur PlaylistBruitages !");
            }
        }
        else
        {
            Debug.LogWarning("GameObject PlaylistBruitages introuvable !");
        }
    }

    //////////////////////////////// DESTRUCTION PROJECTILE /////////////////////////////////////////////////////////////////////////////////////////
    IEnumerator Boom()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
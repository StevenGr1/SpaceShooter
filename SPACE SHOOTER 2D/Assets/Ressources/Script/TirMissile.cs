using System.Collections;
using UnityEngine;

public class TirMissile : MonoBehaviour, IDamageable
{
    public float moveSpeed = 6f;
    public int damage = 1;
    public GameObject Launcher;
    float DespawnTime = 3f;

    public AudioSource audioSource;

    //////////////////////////////// DESPAWN TIR //////////////////////////////////////////////////////////////////////////

    void Start()
    {
        StartCoroutine(Despawn());
        audioSource = GetComponentInParent<AudioSource>();
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Launcher) return;

        if (collision.CompareTag("Player"))
        {
            PlaySoundFromPlaylist(1);
            gameObject.SetActive(false);
            PlayerControler.instance.SetDamage(1, this);
            if (audioSource.isPlaying == false)
            {
                Destroy(gameObject);
            }
        }

        else if (collision.CompareTag("Projectile"))
        {
            PlaySoundFromPlaylist(1);
            Destroy(gameObject);
        }

        else if (collision.CompareTag("Ennemi"))
        {
            if (Launcher == null || !Launcher.CompareTag("Ennemi"))
            {
                PlaySoundFromPlaylist(0);
                PlayerControler.instance.SetPoint(1);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }

        else if (collision.CompareTag("Obstacle"))
        {
            if (Launcher != null && Launcher.CompareTag("Player"))
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
}
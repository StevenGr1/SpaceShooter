using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaylistB : MonoBehaviour
{
    public static PlaylistB instance;
    public AudioClip[] audioClips = new AudioClip[10];
    public AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Plus d'une instance de PlaylistB détectée ! Détruit l'instance en trop.");
            Destroy(gameObject);
            return;
        }
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource introuvable sur l'objet PlaylistB !");
        }
    }

    //////////////////////////////// PLAYLIST /////////////////////////////////////////////////////////////////////////////////////////

    public void PlaySoundFromPlaylist(int index)
    {
        if (audioSource == null) return;

        AudioClip clip = GetAudioClip(index);
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public AudioClip GetAudioClip(int index)
    {
        if (index >= 0 && index < audioClips.Length && audioClips[index] != null)
        {
            return audioClips[index];
        }
        else
        {
            Debug.LogWarning("Index invalide ou AudioClip non assigné !");
            return null;
        }
    }
}
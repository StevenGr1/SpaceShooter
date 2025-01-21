using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaylistB : MonoBehaviour
{
    public AudioClip[] audioClips = new AudioClip[10];

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
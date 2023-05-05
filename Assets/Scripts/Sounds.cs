using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip[] audioClip;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator PlayAudio(int i)
    {
        audioSource.clip = audioClip[i];
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
    }
}

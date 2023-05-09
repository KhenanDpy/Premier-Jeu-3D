using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip[] audioClip;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator PlayAudio(int i)
    {
        audioSource.clip = audioClip[i];

        while (true)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
        }
        
    }
}

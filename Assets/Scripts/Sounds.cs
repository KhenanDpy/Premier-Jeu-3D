using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip[] audioClip;               //!\ A REFAIRE CAR NON FONCTIONNEL. IL FAUT QUE LES SONS VIENNENT DIRECTEMENT DES ACTIONS ET NON D'UN TABLEAU /!\\
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

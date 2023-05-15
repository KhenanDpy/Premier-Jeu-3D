using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A sound plays when collecting a coin
 */

public class CoinSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;

    private void OnTriggerEnter(Collider other)
    {
         audioSource.PlayOneShot(clip);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 * Allows a light to flicker
 */

public class FlickeringLights : MonoBehaviour
{
    private Light flickeringLight;
    System.Random rand = new System.Random();

    void Awake()
    {
        flickeringLight = GetComponent<Light>();
    }

    void OnEnable()
    {
        StartCoroutine(FlickeringLighting());
    }

    IEnumerator FlickeringLighting()
    {
        while (true)
        {
            // intensity is set randomly between 1 and 20 with a 0.05 seconds delay to simulate a flickering effect
            flickeringLight.intensity = rand.Next(1, 20); 
            yield return new WaitForSeconds(0.05f);
        }
    }
}

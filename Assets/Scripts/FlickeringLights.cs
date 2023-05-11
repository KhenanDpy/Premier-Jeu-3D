using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
            flickeringLight.intensity = rand.Next(1, 20);
            Debug.Log("lumière");
            yield return new WaitForSeconds(0.05f);
        }
    }
}

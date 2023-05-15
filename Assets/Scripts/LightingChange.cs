using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 * Allows the light to change when entering in the darkzone
 */

public class LightingChange : MonoBehaviour
{
    public Light directionalLight;
    public Camera lerpCamera;
    public Light[] flickeringLights;

    public int onOff; // 1 = on, -1 = off

    private void Start()
    {
        onOff = 1;
    }

    public void ResetLight()
    {
        // desactivate flickering lights and turn on the skybox
        for (int i = 0; i < flickeringLights.Length; i++)
        {
            flickeringLights[i].gameObject.SetActive(false);
        }
        directionalLight.gameObject.SetActive(true);
        lerpCamera.clearFlags = CameraClearFlags.Skybox;
    }

    private void OnTriggerEnter(Collider other)
    {
        // if triggered, activate flickering lights and turn off the skybox
        if (other.gameObject.CompareTag("Player"))
        {
            onOff *= -1;

            if (onOff == 1)
            {
                ResetLight();
            }

            if (onOff == -1)
            {
                for (int i = 0; i < flickeringLights.Length; i++)
                {
                    flickeringLights[i].gameObject.SetActive(true);
                }
                directionalLight.gameObject.SetActive(false);
                lerpCamera.clearFlags = CameraClearFlags.SolidColor;
            }
        }
    }
}

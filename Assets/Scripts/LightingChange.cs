using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightingChange : MonoBehaviour
{
    public Light directionalLight;
    public Camera lerpCamera;
    public Camera normalCamera;
    public Light[] flickeringLights;

    public int onOff;

    private void Start()
    {
        onOff = 1;
    }

    public void ResetLight()
    {
        for (int i = 0; i < flickeringLights.Length; i++)
        {
            flickeringLights[i].gameObject.SetActive(false);
        }
        directionalLight.gameObject.SetActive(true);
        lerpCamera.clearFlags = CameraClearFlags.Skybox;
        normalCamera.clearFlags = CameraClearFlags.Skybox;
    }

    private void OnTriggerEnter(Collider other)
    {
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
                normalCamera.clearFlags = CameraClearFlags.SolidColor;
            }
        }
    }
}

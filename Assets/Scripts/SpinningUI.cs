using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Allows the object to rotate
 */

public class SpinningUI : MonoBehaviour
{
    int speed = 30;

    void Update()
    {
        // spinning the UI in the final scene
        transform.Rotate(Vector3.up * Time.deltaTime * speed);
    }
}

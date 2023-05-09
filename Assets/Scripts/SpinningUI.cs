using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningUI : MonoBehaviour
{
    int speed = 30;

    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * speed);
    }
}

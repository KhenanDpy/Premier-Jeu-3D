using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movements : MonoBehaviour
{
    [SerializeField]
    Transform cubeTransform;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            cubeTransform.Translate(Vector3.forward*0.02f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            cubeTransform.Translate(Vector3.back*0.02f);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            cubeTransform.Translate(Vector3.left * 0.02f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            cubeTransform.Translate(Vector3.right * 0.02f);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cubeTransform.Translate(Vector3.up * 1f);
        }
    }
}

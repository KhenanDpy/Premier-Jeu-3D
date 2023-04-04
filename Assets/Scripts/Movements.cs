using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movements : MonoBehaviour
{
    [SerializeField]
    Rigidbody cubeRb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            cubeRb.AddForce(transform.forward * 6f, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.S))
        {
            cubeRb.AddForce(transform.forward * -6f, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            cubeRb.AddForce(transform.right * -6f, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.D))
        {
            cubeRb.AddForce(transform.right * 6f, ForceMode.Force);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cubeRb.AddForce(transform.up * 600f, ForceMode.Force);
        }
    }
}

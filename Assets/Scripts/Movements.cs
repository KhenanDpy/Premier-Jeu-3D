using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movements : MonoBehaviour
{
    [SerializeField]
    CharacterController charCC;

    [SerializeField]
    Rigidbody cubeRb;

    [SerializeField]
    float movementSpeed = 0.05f, gravity;

    Vector3 movementDirection = Vector3.zero;

    [SerializeField]
    Camera cameraGO; // permet de bouger le personnage par rapport à la camera

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movementDirection.y -= gravity * Time.deltaTime;
        Debug.Log(movementDirection.y);
        if (Input.GetKey(KeyCode.Z))
        {
            charCC.Move(cameraGO.transform.forward * movementSpeed);
            cubeRb.AddForce(cameraGO.transform.forward * 6f, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.S))
        {
            charCC.Move(cameraGO.transform.forward * -movementSpeed);
            cubeRb.AddForce(cameraGO.transform.forward * -6f, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            charCC.Move(cameraGO.transform.right * -movementSpeed);
            cubeRb.AddForce(cameraGO.transform.right * -6f, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.D))
        {
            charCC.Move(cameraGO.transform.right * movementSpeed);
            cubeRb.AddForce(cameraGO.transform.right * 6f, ForceMode.Force);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            charCC.Move(cameraGO.transform.up * movementSpeed * 50);
            cubeRb.AddForce(cameraGO.transform.up * 300f, ForceMode.Force);
        }
    }
}

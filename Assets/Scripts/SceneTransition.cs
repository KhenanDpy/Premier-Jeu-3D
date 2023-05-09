using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private GameObject player;
    Scene sceneName;

    void Start()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.CompareTag("Player")){
            //SceneManager.MoveGameObjectToScene(player, sceneName);
            DontDestroyOnLoad(player);
            SceneManager.LoadScene("FinalScene");
        }
    }
}

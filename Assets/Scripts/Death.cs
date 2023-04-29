using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    // mourir = plus de PV
    // -> on respawn au début et on recommence la partie

    public Life playerLife;
    public Transform playerPos;
    public Transform respawn;

    void Update()
    {
        if(playerLife.life < 1)
        {
            playerPos = respawn;
            Debug.Log("bipboup");
            playerLife.life = 3;
        }
    }

}

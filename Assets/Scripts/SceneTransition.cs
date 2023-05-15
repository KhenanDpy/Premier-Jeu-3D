using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Allows to transition between 2 scenes. It can adapts to any object it is attached to
 */

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject lerpCamera;
    [SerializeField] private GameObject countdownGO;
    PickUpCoins playerScore;
    FinalStats stats;
    Countdown countdown;
    Attack monstersCount;

    void OnCollisionEnter(Collision collision)
    {
        // load the final scene and keep everything associated with the player to control it in that new scene
        if(collision.collider.gameObject.CompareTag("Player")){
            if (player != null)         // To make any object able to teleport 
            {
                DontDestroyOnLoad(player);
                DontDestroyOnLoad(lerpCamera);

                stats = player.GetComponent<FinalStats>();
                playerScore = player.GetComponent<PickUpCoins>();
                stats.score = playerScore.points;
                countdown = countdownGO.GetComponent<Countdown>();
                stats.timeToFinish = countdown.countdownDuration - countdown.time;
                monstersCount = player.GetComponent<Attack>();
                stats.monsterKilled = monstersCount.enemyKilled;

            }
            else // To return to first scene
            {
                Destroy(GameObject.Find("Player"));
                Destroy(GameObject.Find("LerpCamera"));
            }

            SceneManager.LoadScene(sceneToLoad);

            if (player != null)
            {
                player.transform.position = new Vector3(0, 0, -30); // set the position where the player spawn
            }

        }
    }
}

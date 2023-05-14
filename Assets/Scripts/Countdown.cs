using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    public float time;
    public float countdownDuration = 180f;
    float timerInterval = 60f;
    float tick;
    public Life tooLate;

    float end;

    /* To change scene when time is up */
    [SerializeField] private string sceneToLoad;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject lerpCamera;
    PickUpCoins playerScore;
    FinalStats stats;
    Attack monstersCounter;

    void Awake()
    {
        time = Time.time;
        tick = countdownDuration - timerInterval;
        Reset();
    }

    void Update()
    {
        if (time >= 0f && tooLate.player.alive) // if time is not over && the player is not dead
        {
            GetComponent<TextMeshProUGUI>().text = string.Format("{0:0}:{1:00}", Mathf.Floor(time / 60), time % 60).ToString(); // set the time with the format min:sec
            time = end - Time.time;

            if(time == tick)
            {
                tick = time - timerInterval;
                Debug.Log("timer"); // mettre un son toutes les minutes
            }

            if (time <= 0f)
            {
                //tooLate.player.alive = false;
                DontDestroyOnLoad(player);
                DontDestroyOnLoad(lerpCamera);

                stats = player.GetComponent<FinalStats>();
                playerScore = player.GetComponent<PickUpCoins>();
                stats.score = playerScore.points;
                stats.timeToFinish = countdownDuration - time;
                monstersCounter = player.GetComponent<Attack>();
                stats.monsterKilled = monstersCounter.enemyKilled;


                SceneManager.LoadScene(sceneToLoad);
                player.transform.position = new Vector3(0, 0, -30);

            }
            
        }
        
    }

    public void Reset()
    {
        end = Time.time + countdownDuration;
        time = end - Time.time;
    }
}
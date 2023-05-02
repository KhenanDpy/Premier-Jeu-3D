using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    float time;
    public float countdownDuration = 180f;
    float timerInterval = 60f;
    float tick;
    public Life tooLate;

    float end;

    void Awake()
    {
        time = Time.time;
        tick = countdownDuration - timerInterval;
        Reset();
    }

    void Update()
    {
        if (time >= 0f && tooLate.player.alive)
        {
            GetComponent<TextMeshProUGUI>().text = string.Format("{0:0}:{1:00}", Mathf.Floor(time / 60), time % 60).ToString();
            time = end - Time.time;

            if(time == tick)
            {
                tick = time - timerInterval;
                Debug.Log("timer"); // mettre un son toutes les minutes
            }

            if (time <= 0f)
            {
                tooLate.player.alive = false;
            }
            
        }
        
    }

    public void Reset()
    {
        end = Time.time + countdownDuration;
        time = end - Time.time;
    }
}
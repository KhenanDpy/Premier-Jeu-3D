using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    float time;
    public float countdownDuration = 180f;
    float timerInterval = 60f;
    float tick;

    void Awake()
    {
        time = (int)Time.time;
        tick = timerInterval;
    }

    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = string.Format("{0:0}:{1:00}", Mathf.Floor(time/60), time%60).ToString();
        time = countdownDuration - (int)Time.time;

        if(time == tick)
        {
            tick = time + timerInterval;
            Debug.Log("timer");
        }
    }
}

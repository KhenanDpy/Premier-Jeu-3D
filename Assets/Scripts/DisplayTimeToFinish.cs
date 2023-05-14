using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTimeToFinish : MonoBehaviour
{
    GameObject player;
    float time;
    void Start()
    {
        player = GameObject.Find("Player");
        time = player.GetComponent<FinalStats>().timeToFinish;
        GetComponent<TextMeshProUGUI>().text = "Temps passé \n = \n" + string.Format("{0:0}:{1:00}", Mathf.Floor(time / 60), time % 60).ToString();
    }
}

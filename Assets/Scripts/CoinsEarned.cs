using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsEarned : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        GetComponent<TextMeshProUGUI>().text = "Pi�ces obtenue(s) \n = \n" + player.GetComponent<FinalStats>().score.ToString();
    }
}

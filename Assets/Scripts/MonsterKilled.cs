using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Keep the information about how many monsters the player killed for one run
 */

public class MonsterKilled : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        if (player.GetComponent<FinalStats>().monsterKilled <= 1)
        {
            GetComponent<TextMeshProUGUI>().text = "Monstre tué\n=\n" + player.GetComponent<FinalStats>().monsterKilled.ToString();
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = "Monstres tués\n=\n" + player.GetComponent<FinalStats>().monsterKilled.ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Keep the information about how many coins the player collected for one run
 */

public class CoinsEarned : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        if (player.GetComponent<FinalStats>().score <= 1)
        {
            GetComponent<TextMeshProUGUI>().text = "Pièce obtenue\n=\n" + player.GetComponent<FinalStats>().score.ToString();
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = "Pièces obtenues\n=\n" + player.GetComponent<FinalStats>().score.ToString();
        }

    }
}

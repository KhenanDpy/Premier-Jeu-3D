using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonsterKilled : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        GetComponent<TextMeshProUGUI>().text = "Monstres tué(s) \n = \n" + player.GetComponent<FinalStats>().monsterKilled.ToString();
    }
}

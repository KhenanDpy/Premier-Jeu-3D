using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    private GameObject coin;
    public GameObject[] coinType;
    public GameObject platformsParent;
    public Attack monsterKilled;
    System.Random rand = new System.Random();
    int children;

    int goldenThreshold;

    /* pour accéder à la dark zone */
    public bool coinGoalReached;
    public GameObject darkZoneGate;

    void Start()
    {
        children = platformsParent.transform.childCount;
        goldenThreshold = 10;
        Invoke("Spawn", 3f);
    }

    void Spawn()
    {
        int randomSpawn = rand.Next(children);
        int randomCoinType = rand.Next(10);
        int spawnDelay = rand.Next(3, 5);
        
        if (randomCoinType > goldenThreshold)
        {
            coin = coinType[0];
        }
        else
        {
            coin = coinType[1];
        }

        if (platformsParent.transform.GetChild(randomSpawn).transform.childCount == 1)
        {
            var coins = Instantiate(coin);//,transform.position, transform.rotation); // platformsParent.transform.GetChild(randomSpawn).position, platformsParent.transform.GetChild(randomSpawn).rotation);

            GameObject inter = new GameObject();
            inter.transform.parent = platformsParent.transform.GetChild(randomSpawn).transform;

            inter.transform.localPosition = Vector3.zero + Vector3.up * 2.0f;

            coins.transform.parent = inter.transform;

            Destroy(platformsParent.transform.GetChild(randomSpawn).transform.GetChild(1).transform.gameObject, 10f);
        }

        if (coinGoalReached)
        {
            darkZoneGate.SetActive(false);
        }

        Invoke("Spawn", spawnDelay);

    }

    public void RemoveAll()
    {
        for (int i = 0; i < children; i++)
        {
            if (platformsParent.transform.GetChild(i).transform.childCount > 1)
                Destroy(platformsParent.transform.GetChild(i).transform.GetChild(1).transform.gameObject);
        }
    }

    void Update()
    {
        goldenThreshold = 10 - (int)(monsterKilled.enemyKilled / 1.5f); 
    }
}

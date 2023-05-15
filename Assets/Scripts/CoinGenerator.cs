using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Generate randomly chosen coins to a random platform from the scene
 */
public class CoinGenerator : MonoBehaviour
{
    private GameObject coin;
    public GameObject[] coinType; // [0] = gold, [1] = silver
    public GameObject platformsParent;
    public Attack monsterKilled;
    System.Random rand = new System.Random();
    int children;

    int goldenThreshold; // to limit the spawn of gold coins

    /* to access the dark zone */
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

        if (platformsParent.transform.GetChild(randomSpawn).transform.childCount == 1) // limit so the coins can't spawn on a platform that already has a coin
        {
            var coins = Instantiate(coin);

            GameObject inter = new GameObject();
            inter.transform.parent = platformsParent.transform.GetChild(randomSpawn).transform;

            inter.transform.localPosition = Vector3.zero + Vector3.up * 2.0f; // to place the coin above the platform (else it is instantiate inside the platform)

            coins.transform.parent = inter.transform;

            Destroy(platformsParent.transform.GetChild(randomSpawn).transform.GetChild(1).transform.gameObject, 10f); // the coin is destroyed after 10 sec 
        }

        if (coinGoalReached)
        {
            darkZoneGate.SetActive(false);
        }

        Invoke("Spawn", spawnDelay);

    }

    // remove every coins of the scene
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
        goldenThreshold = 10 - (int)(monsterKilled.enemyKilled / 1.5f); // the more monster you kill, the more gold coins appears
    }
}

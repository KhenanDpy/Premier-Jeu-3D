using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    private GameObject coin;
    public GameObject[] coinType;
    public GameObject platformsParent;
    System.Random rand = new System.Random();
    int children;

    void Start()
    {
        children = platformsParent.transform.childCount;
        Invoke("Spawn", 3f);
    }

    void Update()
    {
  
    }

    void Spawn()
    {
        int randomSpawn = rand.Next(children);
        int randomCoinType = rand.Next(10);
        int spawnDelay = rand.Next(3, 5);

        if (randomCoinType > 8)
        {
            coin = coinType[0];
        }
        else
        {
            coin = coinType[1];
        }

        if (platformsParent.transform.GetChild(randomSpawn).transform.childCount == 0)
        {
            var coins = Instantiate(coin);//,transform.position, transform.rotation); // platformsParent.transform.GetChild(randomSpawn).position, platformsParent.transform.GetChild(randomSpawn).rotation);

            GameObject inter = new GameObject();
            inter.transform.parent = platformsParent.transform.GetChild(randomSpawn).transform;

            inter.transform.localPosition = Vector3.zero + Vector3.up * 2.0f;

            coins.transform.parent = inter.transform;

            Destroy(platformsParent.transform.GetChild(randomSpawn).transform.GetChild(0).transform.gameObject, 5f);
        }
        Invoke("Spawn", spawnDelay);

    }

    public void RemoveAll()
    {
        for (int i = 0; i < children; i++)
        {
            if (platformsParent.transform.GetChild(i).transform.childCount > 0)
                Destroy(platformsParent.transform.GetChild(i).transform.GetChild(0).transform.gameObject);
        }
    }
}

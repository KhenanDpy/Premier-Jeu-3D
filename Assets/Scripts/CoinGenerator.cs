using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = System.Random;

public class CoinGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject coin;
    public GameObject platformsParent;
    Random rand = new Random();
    int children;

    void Start()
    {
       children = platformsParent.transform.childCount;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            int randomSpawn = rand.Next(children);
            var coins = Instantiate(coin,transform.position, transform.rotation); // platformsParent.transform.GetChild(randomSpawn).position, platformsParent.transform.GetChild(randomSpawn).rotation);
            Debug.Log(platformsParent.transform.GetChild(randomSpawn).name + platformsParent.transform.GetChild(randomSpawn).position);
            coins.transform.parent = platformsParent.transform.GetChild(randomSpawn).transform;
            coins.transform.position +=  UnityEngine.Vector3.up * 2.0f;
            
        }
    }
}

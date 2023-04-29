using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject coin;
    public GameObject platformsParent;
    System.Random rand = new System.Random();
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
            var coins = Instantiate(coin);//,transform.position, transform.rotation); // platformsParent.transform.GetChild(randomSpawn).position, platformsParent.transform.GetChild(randomSpawn).rotation);

            GameObject inter = new GameObject();
            inter.transform.parent = platformsParent.transform.GetChild(randomSpawn).transform;
            
            inter.transform.localPosition =  Vector3.zero + Vector3.up * 2.0f;
            
            coins.transform.parent = inter.transform;
        }
    }
}

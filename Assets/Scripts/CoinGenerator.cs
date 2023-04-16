using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject Coin;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            var coins = Instantiate(Coin, transform.position, transform.rotation);

            coins.transform.parent = transform;
        }
    }
}

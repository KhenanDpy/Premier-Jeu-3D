using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

/*
 * Allows the monsters to move on the platform they are on. They also have 2 way to move.
 * They move to a random position or they chase the player.
 */

public class MonsterNavMesh : MonoBehaviour
{
    [SerializeField] private Transform _movePosTransform;
    [SerializeField] private Transform player;

    NavMeshAgent _navMeshAgent;

    Random rand = new Random();

    /* for the field of view */
    bool chase;

    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        StartCoroutine(MyUpdate());
    }

    IEnumerator MyUpdate()
    {
        while (true)
        {
            if (chase)
            {
                // following the player
                transform.GetChild(4).transform.gameObject.SetActive(false); // desactivate the field of view trigger
                yield return new WaitForSeconds(2); // wait for 2 seconds
                chase = false;
                transform.GetChild(4).transform.gameObject.SetActive(true); // activate the field of view trigger
                // if the player is still on the field of view, the monster's chase mode is activate
            }
            else
            {
                float seconds = rand.Next(3,7); // wait between 3 and 7 seconds before changing the monster destination
                yield return new WaitForSeconds(seconds);
                _movePosTransform.transform.position = new Vector3(rand.Next(Convert.ToInt32(transform.position.x - 20), Convert.ToInt32(transform.position.x + 20)),
                                                                   0,
                                                                   rand.Next(Convert.ToInt32(transform.position.z - 20), Convert.ToInt32(transform.position.z + 20)));
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // if the player is in the range, chase mode activated
        {
            chase = true;
        }
    }

    void Update()
    {
        if (chase)
        {
            // the monster follow the player
            _navMeshAgent.destination = player.position;
        }
        else
        {
            // the monster move to a random position
            _navMeshAgent.destination = _movePosTransform.position;
        }
    }
}

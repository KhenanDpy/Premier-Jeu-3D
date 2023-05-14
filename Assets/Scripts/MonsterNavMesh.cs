using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class MonsterNavMesh : MonoBehaviour
{
    [SerializeField] private Transform _movePosTransform;
    [SerializeField] private Transform player;
    //[SerializeField] private Camera _camera;
    //[SerializeField] private GameObject _plane;

    NavMeshAgent _navMeshAgent;

    /*int planeX;
    int planeZ;*/

    Random rand = new Random();

    /* for the field of view */
    bool pursuit;

    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        /*int planeX = Convert.ToInt32(_plane.transform.localScale.x);      J'ai essayé de récupérer la taille du plan mais ça m'affiche 0,0,0.
        int planeZ = Convert.ToInt32(_plane.transform.localScale.z);*/
    }

    void Start()
    {
        StartCoroutine(MyUpdate());
        //StartCoroutine(ChaseMode());
    }

    float timerTime;

   /* IEnumerator ChaseMode()
    {
        if (pursuit)
        {
            transform.GetChild(5).transform.gameObject.SetActive(false);
            yield return new WaitForSeconds(2);
            transform.GetChild(5).transform.gameObject.SetActive(true);
            // on suit le personnage
            _navMeshAgent.destination = player.position;
            Debug.Log("a bougé");
        }
    }*/
    IEnumerator MyUpdate()
    {
        while (true)
        {
            if (pursuit)
            {
                // following the player
                transform.GetChild(4).transform.gameObject.SetActive(false); // desactivate the field of view trigger
                yield return new WaitForSeconds(2); // wait for 2 seconds
                pursuit = false;
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
        if (other.gameObject.CompareTag("Player"))
        {
            pursuit = true;
        }
    }

    void Update()
    {
        if (pursuit)
        {
            // on suit le personnage
            _navMeshAgent.destination = player.position;
        }
        else
        {
            _navMeshAgent.destination = _movePosTransform.position;
        }

        /*timerTime += Time.deltaTime;

        if (timerTime >= 3)
        {
            timerTime = 0;
            Debug.Log("pop");
            _movePosTransform.transform.position = new Vector3(rand.Next(-20, 20), 0, rand.Next(-20, 20)); //rand.Next(-planeX, planeX), 0, rand.Next(-planeZ, planeZ)); avec la scale du plan.
            _navMeshAgent.destination = _movePosTransform.position;
        }*/             // Autre genre de timer

        /*if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 100))   // Pour bouger en cliquant avec la souris
            {
                _movePosTransform.position = hit.point;
                _navMeshAgent.destination = _movePosTransform.position;
            }
        }*/
    }
}

using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class MonsterNavMesh : MonoBehaviour
{
    [SerializeField] private Transform _movePosTransform;
    //[SerializeField] private Camera _camera;
    [SerializeField] private GameObject _plane;

    NavMeshAgent _navMeshAgent;

    /*int planeX;
    int planeZ;*/

    Random rand = new Random();

    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        /*int planeX = Convert.ToInt32(_plane.transform.localScale.x);      J'ai essayé de récupérer la taille du plan mais ça m'affiche 0,0,0.
        int planeZ = Convert.ToInt32(_plane.transform.localScale.z);*/
    }

    void Start()
    {
        StartCoroutine(MyUpdate());
    }

    float timerTime;

    IEnumerator MyUpdate()
    {
        while (true)
        {
            float seconds = rand.Next(3,7); // On attend entre 3 et 7 secondes avant de faire changer la position du monstre
            Debug.Log(seconds);
            yield return new WaitForSeconds(seconds);
            _movePosTransform.transform.position = new Vector3(rand.Next(Convert.ToInt32(transform.position.x - 20), Convert.ToInt32(transform.position.x + 20)),
                                                               0, 
                                                               rand.Next(Convert.ToInt32(transform.position.x - 20), Convert.ToInt32(transform.position.x + 20)));
            _navMeshAgent.destination = _movePosTransform.position;

        }
    }

    void Update()
    {

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

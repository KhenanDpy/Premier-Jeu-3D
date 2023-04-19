using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class MonsterNavMesh : MonoBehaviour
{
    [SerializeField] private Transform _movePosTransform;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _plane;

    NavMeshAgent _navMeshAgent;

    int i;

    Random rand = new Random();

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private int Timer()
    {
        i++;
        if(i == 1001)
        {
            i = 0;
        }

        return i;
    }

    void Update()
    {
        Timer();

        if (Timer() == 1000)
        {
            Debug.Log("pop");
            _movePosTransform.transform.position = new Vector3(rand.Next(-20,20), 0, rand.Next(-20,20));
        }
        _navMeshAgent.destination = _movePosTransform.position;

        /*if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 100))   // Pour bouger en cliquand avec la souris
            {
                _movePosTransform.position = hit.point;
                _navMeshAgent.destination = _movePosTransform.position;
            }
        }*/
    }
}

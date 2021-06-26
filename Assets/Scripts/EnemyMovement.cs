using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    public Transform[] wayPoints;
    public Transform currentWaypointDest;
    private Vector3 _currentDestPos;
    private Transform _currentDest;
    private Transform _nextDest;
    private EnemyState _currentState;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (_navMeshAgent != null)
        {
            _currentDest = wayPoints[Random.Range(0, wayPoints.Length)];
            _currentDestPos = _currentDest.position;
            _navMeshAgent.destination = _currentDestPos;
        }
    }

    private void FixedUpdate()
    {
        if (!_navMeshAgent.pathPending)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    ChooseNewDestination();
                }
            }
        }
    }

    void ChooseNewDestination()
    {
        do
        {
            _nextDest = wayPoints[Random.Range(0, wayPoints.Length)];
            _currentDestPos = _nextDest.position;
            _navMeshAgent.destination = _currentDestPos;
            ChangeCurrentWaypointPos(_currentDestPos);
        } while (_currentDest == _nextDest);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShootRayCast(other);
        }
    }

    void ShootRayCast(Collider player)
    {
        if (CheckInFront(player))
        {
            Vector3 directionToTarget = player.transform.position - transform.position;
            RaycastHit hit;
            bool raycast = Physics.Raycast(transform.position,
                directionToTarget, out hit, 20);
            if (raycast)
            {
                Debug.DrawRay(transform.position, directionToTarget, Color.blue);
                Debug.Log("Hit: " + hit.collider.name);
                if (hit.transform.CompareTag("Player"))
                {
                    Debug.Log("XD");
                }
            }
        }
    }

    bool CheckInFront(Collider other)
    {
        Vector3 directionToTarget = transform.position - other.transform.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);
        return Mathf.Abs(angle) > 100;
    }

    void ChangeCurrentWaypointPos(Vector3 pos)
    {
        currentWaypointDest.transform.position = pos;
    }
}
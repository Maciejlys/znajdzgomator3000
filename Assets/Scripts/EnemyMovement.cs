using System;
using System.Collections;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    public Transform[] wayPoints;
    public Transform currentWaypointDest;
    private Vector3 _currentDestPos;
    private Transform _currentDest, _nextDest;
    private Vector3 _lastPlayerPos;
    private EnemyState _currentState;

    void Start()
    {
        //Debug state printer
        StartCoroutine("StatePrinter");


        _navMeshAgent = GetComponent<NavMeshAgent>();
        _currentState = EnemyState.Patroling;
        if (_navMeshAgent != null)
        {
            _currentDest = wayPoints[Random.Range(0, wayPoints.Length)];
            _currentDestPos = _currentDest.position;
            _navMeshAgent.destination = _currentDestPos;
            ChangeCurrentWaypointPos(_currentDestPos);
        }
    }

    IEnumerator StatePrinter() //Debug state printer
    {
        while (true)
        {
            Debug.Log(_currentState);
            yield return new WaitForSeconds(1f);
        }
    }

    private void FixedUpdate()
    {
        if (_currentState == EnemyState.Patroling) Patroling();
        else if (_currentState == EnemyState.Chasing) Chasing();
        switch (_currentState)
        {
            case EnemyState.Patroling:
                Patroling();
                break;
            case EnemyState.Chasing:
                Chasing();
                break;
            case EnemyState.LookingAround:
                LookAround();
                break;
            case EnemyState.Evade:
                break;
        }
    }

    private void LookAround()
    {
        _currentState = EnemyState.Evade;
        StartCoroutine(Rotate(5f));
    }

    IEnumerator Rotate(float duration)  // Enables the NPC to turn around
    {
        bool flag = false;
        Quaternion startRot = transform.rotation;
        float t = 0f;
        while (t < duration)
        {
            if (_currentState != EnemyState.Evade) //Stops the rotation in case player has been spotted
            {
                flag = true;
                break;
            }
            t += Time.deltaTime;
            transform.rotation = startRot * Quaternion.AngleAxis(t / duration * 360f, Vector3.up);
            yield return null;
        }

        if(!flag) transform.rotation = startRot;    // flag prevents enemy from weird snapping while rotating.
        yield return new WaitForSeconds(1f);
        _currentState = EnemyState.Patroling;
    }

    void Chasing()
    {
        StopCoroutine("Rotate");
        _navMeshAgent.destination = _lastPlayerPos;
        ChangeCurrentWaypointPos(_lastPlayerPos);

        if (CheckIfOnWaypoint())    // After NPC gets to last seen player position and state wasn't changed it looks around.
        {
            _currentState = EnemyState.LookingAround;
        }
    }

    void Patroling()
    {
        if (CheckIfOnWaypoint()) ChooseNewDestination();
    }

    bool CheckIfOnWaypoint()    // Checks if NPC came to its destination
    {
        if (!_navMeshAgent.pathPending)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
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
                if (hit.transform.CompareTag("Player"))
                {
                    //Do stuff, enemy sees the player
                    _currentState = EnemyState.Chasing;
                    _lastPlayerPos = player.transform.position;
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
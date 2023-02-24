using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationExample : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent thisAgent;


    private void Awake()
    {
        thisAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        thisAgent.SetDestination(target.transform.position);
    }
}

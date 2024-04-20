using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleAiMouvement : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;

    zombie zombieScript;
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        zombieScript = GetComponent<zombie>();
    }

    
    void Update()
    {
        if (!zombieScript.getIsDead())
        {
            agent.SetDestination(target.position);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutoHandler : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("Zombie");

        foreach (GameObject go in list)
        {
            go.GetComponent<SimpleAiMouvement>().enabled = false;
            go.GetComponent<Animator>().enabled = false;
            go.GetComponent<NavMeshAgent>().enabled = false;
        }
    }
}

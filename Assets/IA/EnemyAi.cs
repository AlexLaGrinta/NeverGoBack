using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;



public class EnnemiSuiviNavMesh : MonoBehaviour
{
    private NavMeshAgent navAgent;

    [SerializeField]
    private int distAttack = 5;

    private Animator MobAnimator;


    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        MobAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length == 0) return;



        Transform joueurProche = players[0].transform;
        float minDist = Vector3.Distance(transform.position, joueurProche.position);

        foreach (var j in GameObject.FindGameObjectsWithTag("Player"))
        {
            Transform joueur = j.transform;
            float dist = Vector3.Distance(transform.position, joueur.position);
            if (dist < minDist)
            {
                minDist = dist;
                joueurProche = joueur;
            }
        }

        if (minDist > 180)
        {
            navAgent.SetDestination(transform.position);
            MobAnimator.SetBool("isRunning", false);
            return;
        }

        if (minDist < distAttack) {
            MobAnimator.SetBool("isRunning", false);
            navAgent.SetDestination(transform.position); }
        else {
            MobAnimator.SetBool("isRunning", true);
            navAgent.SetDestination(joueurProche.position);
        }
    }
}



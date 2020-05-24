using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PeopleWalk : MonoBehaviour
{
    public Transform[] walk_Points;
    public float walk_speed = 1f;
    public bool isIdle;

    private int walk_Index;

    private NavMeshAgent navAgent;
    private Animator anim;

    
    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

     void Start()
    {
        if (isIdle)
        {
            anim.Play("Idle");
        }
        else
        {
            anim.Play("Run");
        }
    }

    
    void Update()
    {
        if (!isIdle)
        {
            ChooseWalkPoints();
        }
    }

    void ChooseWalkPoints()
    {
        if(navAgent.remainingDistance <= 0.1f)
        {
            navAgent.SetDestination(walk_Points[walk_Index].position);

            if(walk_Index == walk_Points.Length - 1)
            {
                walk_Index = 0;
            }
            else
            {
                walk_Index++;
            }
        }
    }
}

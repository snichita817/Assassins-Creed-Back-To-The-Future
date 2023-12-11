using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBehavior : StateMachineBehaviour
{
    float timer;
    List<Transform> points = new List<Transform>();
    NavMeshAgent agent;
    int nextPointIndex = 0; // Added a variable to keep track of the next point index

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        Transform pointsObject = GameObject.FindGameObjectWithTag("Route").transform;

        foreach (Transform t in pointsObject)
            points.Add(t);

        agent = animator.GetComponent<NavMeshAgent>();

        if (points.Count > 0)
            agent.SetDestination(points[nextPointIndex].position);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(agent.remainingDistance);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            nextPointIndex = (nextPointIndex + 1) % points.Count; // Cycle through the points
            agent.SetDestination(points[nextPointIndex].position);
        }

        timer += Time.deltaTime;
        if (timer > 10)
            animator.SetBool("isPatrolling", false);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }
}

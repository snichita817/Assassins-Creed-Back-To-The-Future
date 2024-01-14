using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehavior2 : StateMachineBehaviour
{
    public string index = "";
    public int patrolTimer;
    float timer;
    List<Transform> points = new List<Transform>();
    UnityEngine.AI.NavMeshAgent agent;

    Transform player;
    float chaseRange = 10;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        Transform pointsObject = GameObject.FindGameObjectWithTag("Route" + index).transform;
        foreach (Transform t in pointsObject)
            points.Add(t);

        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(points[0].position);

        // Initialize the player variable, but don't rely on it for up-to-date position
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
            agent.SetDestination(points[Random.Range(0, points.Count)].position);

        timer += Time.deltaTime;
        if (timer > patrolTimer)
            animator.SetBool("isPatrolling", false);

        // Update the player's position continuously
        float distance = Vector3.Distance(animator.transform.position, player.position);
        // Debug.Log(animator.transform.position + " " + player.position + " " + distance);
        if (distance < chaseRange)
            animator.SetBool("isChasing", true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }
}

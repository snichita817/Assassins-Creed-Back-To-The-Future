using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEnemyBehavior : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(animator.transform.position, player.transform.position);
        if (animator.GetBool("isAttacking"))
        {
            Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - enemy.transform.position);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, Time.deltaTime * 50f);
        }
    }
}

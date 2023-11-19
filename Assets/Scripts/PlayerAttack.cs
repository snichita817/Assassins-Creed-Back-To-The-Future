using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 3f;
    public float attackDelay = 0.3f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;

    public LayerMask attackLayer;

    // need to find audio files first
    // public AudioClip attackSwing;
    // public AudioClip hitSound;

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;

    public void Attack()
    {
        Debug.Log("Attack");
        if(!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);

        if(attackCount == 0)
        {
            attackCount++;
        }
        else{
            attackCount = 0;
        }
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    void AttackRaycast()
    {
        Camera cam = GetComponent<PlayerLook>().cam;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackRange, attackLayer)){
            HitTarget(hit.point);

            if(hit.transform.TryGetComponent<Actor>(out Actor Target))
            {
                Target.TakeDamage(attackDamage);
            }
        }
    }

    void HitTarget(Vector3 pos)
    {
        //Create a collision volume to detect attack range
        GameObject collisionVolume = new GameObject("CollisionVolume");
        collisionVolume.transform.position = pos;

        //Add a SphereCollider to the GameObject
        SphereCollider sphereCollider = collisionVolume.AddComponent<SphereCollider>();
        sphereCollider.radius = attackRange;
        //isTrigger true so it allows collision detection but you can pass through the collider
        sphereCollider.isTrigger = true;
        Destroy(collisionVolume, .1f);
    }
}

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
    public GameObject attackEffect;

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

    void Update() 
    {

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
        // audioSource.pitch = 1;
        // AudioSource.PlayOneShot(hitSound);

        GameObject GO = Instantiate(attackEffect, pos, Quaternion.identity);
        Destroy(GO, 20);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}

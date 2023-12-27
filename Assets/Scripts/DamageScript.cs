using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public int damageAmount = 10;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        PlayerManager.Damage(damageAmount);    
    }
}
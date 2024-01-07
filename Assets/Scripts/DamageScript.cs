using System.Collections;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public int damageAmount = 10;
    public float damageCooldown = 1f; 
    private bool canDealDamage = true;


    private void OnTriggerStay(Collider other)
    {
        if (canDealDamage && other.CompareTag("Player"))
        {
            PlayerManager.Damage(damageAmount);
            StartCoroutine(DamageCooldown());
        }
    }

    IEnumerator DamageCooldown()
    {
        // prevent further damage during cooldown
        canDealDamage = false;

        // Wait for the cooldown period
        yield return new WaitForSeconds(damageCooldown);
        canDealDamage = true;
    }
}

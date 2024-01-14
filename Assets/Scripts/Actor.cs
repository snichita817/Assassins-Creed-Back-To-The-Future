using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField]
    int currentHealth;
    public int maxHealth;
    public GameObject SaveLoader, Portal01, Portal02;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        Debug.Log(currentHealth);
        currentHealth -= amount;

        if(currentHealth <= 0)
        { Death(); }
    }

    void Death()
    {
        // Death function
        // TEMPORARY: Destroy Object
        ++SaveLoader.GetComponent<LoadSaveScript>().kills;
        Destroy(gameObject);

        if (SaveLoader.GetComponent<LoadSaveScript>().kills == 2)
        {
            Portal02.transform.position = new Vector3(-61.5f, 3.18f, 280.1f);
        }
    }
}
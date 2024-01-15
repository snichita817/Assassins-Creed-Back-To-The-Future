using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class Actor : MonoBehaviour
{
    [SerializeField]
    int currentHealth;
    public int maxHealth;
    public GameObject SaveLoader, portal01, portal02, player;
    public int index;
    public PlayerManager PlayerManager;

    void Awake()
    {
        currentHealth = maxHealth;
        if (SaveGame.Load<bool>("actor" + index, true) == false)
        {
            Destroy(gameObject);
        }
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
        SaveLoader.GetComponent<LoadSaveScript>().SavePlayerPositionData();

        SaveGame.Save<bool>("actor" + index, false);

        if (SaveLoader.GetComponent<LoadSaveScript>().kills == 2)
        {
            portal02.transform.position = new Vector3(-61.5f, 3.18f, 280.1f);
        } else if (SaveLoader.GetComponent<LoadSaveScript>().kills == 7)
        {
            portal02.transform.position = new Vector3(-120f, 26f, -31f);
        }
        else if (SaveLoader.GetComponent<LoadSaveScript>().kills == 13)
        {
            portal01.transform.position = new Vector3(-122f, 2.75f, 54f);
            portal02.transform.position = new Vector3(-182f, 3.74f, -114f);
        }

        float portal01X = portal01.transform.position.x;
        float portal01Y = portal01.transform.position.y;
        float portal01Z = portal01.transform.position.z;
        float portal02X = portal02.transform.position.x;
        float portal02Y = portal02.transform.position.y;
        float portal02Z = portal02.transform.position.z;

        SaveGame.Save<float>("portal01X", portal01X);
        SaveGame.Save<float>("portal01Y", portal01Y);
        SaveGame.Save<float>("portal01Z", portal01Z);
        SaveGame.Save<float>("portal02X", portal02X);
        SaveGame.Save<float>("portal02Y", portal02Y);
        SaveGame.Save<float>("portal02Z", portal02Z);

        SaveGame.Save<int>("playerHealth", PlayerManager.getHP());
    }
}
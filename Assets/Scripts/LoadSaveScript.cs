using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class LoadSaveScript : MonoBehaviour
{
    public GameObject Player;
    public int kills;
    float PlayerX, PlayerY, PlayerZ;
    bool start = true;
    public GameObject portal01, portal02;
    public PlayerManager PlayerManager;

    void Start()
    {
        StartCoroutine(DisableInputManagerThenAutoSave());

        PlayerX = SaveGame.Load<float>("PlayerX", -11f);
        PlayerY = SaveGame.Load<float>("PlayerY", 2f);
        PlayerZ = SaveGame.Load<float>("PlayerZ", -83f);
        kills = SaveGame.Load<int>("kills", 0f);

        Player.transform.position = new Vector3(PlayerX, PlayerY, PlayerZ);
        //Debug.Log("Load Player Position: " + PlayerX + ", " + PlayerY + ", " + PlayerZ);

        float portal01X = SaveGame.Load<float>("portal01X", portal01.transform.position.x);
        float portal01Y = SaveGame.Load<float>("portal01Y", portal01.transform.position.y);
        float portal01Z = SaveGame.Load<float>("portal01Z", portal01.transform.position.z);
        float portal02X = SaveGame.Load<float>("portal02X", portal02.transform.position.x);
        float portal02Y = SaveGame.Load<float>("portal02Y", portal02.transform.position.y);
        float portal02Z = SaveGame.Load<float>("portal02Z", portal02.transform.position.z);
        portal01.transform.position = new Vector3(portal01X, portal01Y, portal01Z);
        portal02.transform.position = new Vector3(portal02X, portal02Y, portal02Z);
    }

    IEnumerator DisableInputManagerThenAutoSave()
    {
        InputManager inputManager = Player.GetComponent<InputManager>();
        if (inputManager != null)
        {
            inputManager.enabled = false;
        }

        yield return StartCoroutine(AutoSave());

        if (inputManager != null)
        {
            inputManager.enabled = true;
        }
    }

    IEnumerator AutoSave()
    {
        InputManager inputManager = Player.GetComponent<InputManager>();
        while (true)
        {
            if (start && inputManager != null)
            {
                start = false;
                inputManager.enabled = false;
                yield return new WaitForSeconds(.1f);
            } else
            {
                yield return new WaitForSeconds(5f);
            }

            
            SavePlayerPositionData();

            if (inputManager != null)
            {
                inputManager.enabled = true;
            }
        }
    }

    public void SavePlayerPositionData()
    {
        float playerX = Player.transform.position.x;
        float playerY = Player.transform.position.y;
        float playerZ = Player.transform.position.z;

        SaveGame.Save<float>("PlayerX", playerX);
        SaveGame.Save<float>("PlayerY", playerY);
        SaveGame.Save<float>("PlayerZ", playerZ);
        SaveGame.Save<float>("kills", kills);

        Debug.Log("Saved Player Position: " + playerX + ", " + playerY + ", " + playerZ);
    }

}

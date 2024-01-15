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

    public void StartC()
    {
        StartCoroutine(DisableInputManagerThenAutoSave());
    }

    void Start()
    {
        PlayerX = SaveGame.Load<float>("PlayerX", -11f);
        PlayerY = SaveGame.Load<float>("PlayerY", 2f);
        PlayerZ = SaveGame.Load<float>("PlayerZ", -83f);
        kills = SaveGame.Load<int>("kills", 0f);

        Player.transform.position = new Vector3(PlayerX, PlayerY, PlayerZ);
        //Debug.Log("Load Player Position: " + PlayerX + ", " + PlayerY + ", " + PlayerZ);

        float portal01X = SaveGame.Load<float>("portal01X", 37.6f);
        float portal01Y = SaveGame.Load<float>("portal01Y", 2.75f);
        float portal01Z = SaveGame.Load<float>("portal01Z", 19.99f);
        float portal02X = SaveGame.Load<float>("portal02X", -5f);
        float portal02Y = SaveGame.Load<float>("portal02Y", 2.75f);
        float portal02Z = SaveGame.Load<float>("portal02Z", -114f);
        portal01.transform.position = new Vector3(portal01X, portal01Y, portal01Z);
        portal02.transform.position = new Vector3(portal02X, portal02Y, portal02Z);

        if (SaveGame.Load<bool>("reset", false) == true)
        {
            SaveGame.Save<bool>("reset", false);
            SaveGame.Clear();
            PlayerX = -11f;
            PlayerY = 2f;
            PlayerZ = -83f;
            kills = 0;
            Player.transform.position = new Vector3(PlayerX, PlayerY, PlayerZ);

            portal01X = 37.6f;
            portal01Y = 2.75f;
            portal01Z = 19.99f;
            portal02X = -5f;
            portal02Y = 2.75f;
            portal02Z = -114f;
            portal01.transform.position = new Vector3(portal01X, portal01Y, portal01Z);
            portal02.transform.position = new Vector3(portal02X, portal02Y, portal02Z);
        }
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

    public IEnumerator AutoSave()
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
                yield return new WaitForSeconds(.2f);
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
    }

}

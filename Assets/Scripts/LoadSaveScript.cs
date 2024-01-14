using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class LoadSaveScript : MonoBehaviour
{
    public GameObject Player;
    float PlayerX, PlayerY, PlayerZ;
    bool start = true;

    void Start()
    {
        StartCoroutine(DisableInputManagerThenAutoSave());

        PlayerX = SaveGame.Load<float>("PlayerX", -6f);
        PlayerY = SaveGame.Load<float>("PlayerY", 2f);
        PlayerZ = SaveGame.Load<float>("PlayerZ", -105f);

        Player.transform.position = new Vector3(PlayerX, PlayerY, PlayerZ);
        //Debug.Log("Load Player Position: " + PlayerX + ", " + PlayerY + ", " + PlayerZ);
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

    void SavePlayerPositionData()
    {
        float playerX = Player.transform.position.x;
        float playerY = Player.transform.position.y;
        float playerZ = Player.transform.position.z;

        SaveGame.Save<float>("PlayerX", playerX);
        SaveGame.Save<float>("PlayerY", playerY);
        SaveGame.Save<float>("PlayerZ", playerZ);

        Debug.Log("Saved Player Position: " + playerX + ", " + playerY + ", " + playerZ);
    }

}

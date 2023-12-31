using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static int playerHealth;
    public static bool gameOver;
    public TextMeshProUGUI playerHealthText;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 100;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        // update the player health
        playerHealthText.text = "" + playerHealth;
        Debug.Log("Player Health: " + playerHealth);
        if(gameOver)
        {
            // after player death load this particular scene
            SceneManager.LoadScene("firstLevel");
        }
    }

    public static void Damage(int damageAmount)
    {
        playerHealth -= damageAmount;

        if(playerHealth <= 0)
        {
            // player is dead
            gameOver = true;
        }
    }
}

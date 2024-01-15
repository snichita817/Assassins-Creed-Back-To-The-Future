using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;


public class NPCController : MonoBehaviour
{
    public Canvas messageCanvas;
    public GameObject Player;
    public PlayerManager PlayerManager;
    public GameObject SaveLoader, portal01;

    private void Start()
    {
        messageCanvas.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageCanvas.gameObject.SetActive(true);
            if (name == "npc4" || name == "npc6")
            {
                PlayerManager.HealMax();
                if (name == "npc4")
                {
                    SaveLoader.GetComponent<LoadSaveScript>().kills = 3;
                    portal01.transform.position = new Vector3(-94f, 2.75f, 196f);

                    float portal01X = portal01.transform.position.x;
                    float portal01Y = portal01.transform.position.y;
                    float portal01Z = portal01.transform.position.z;

                    SaveGame.Save<float>("portal01X", portal01X);
                    SaveGame.Save<float>("portal01Y", portal01Y);
                    SaveGame.Save<float>("portal01Z", portal01Z);
                } else
                {
                    SaveLoader.GetComponent<LoadSaveScript>().kills = 8;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           messageCanvas.gameObject.SetActive(false);
        }
    }
}

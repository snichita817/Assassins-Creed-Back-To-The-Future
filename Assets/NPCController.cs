using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    public Canvas messageCanvas;
    public GameObject Player;
    public PlayerManager PlayerManager;
    public GameObject SaveLoader, Portal01;

    private void Start()
    {
        messageCanvas.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageCanvas.gameObject.SetActive(true);
            if (name == "npc4")
            {
                PlayerManager.HealMax();
                SaveLoader.GetComponent<LoadSaveScript>().kills = 3;

                Portal01.transform.position = new Vector3(-94f, 2.75f, 196f);
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

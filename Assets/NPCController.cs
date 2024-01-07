using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    public Canvas messageCanvas;

    private void Start()
    {
        messageCanvas.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageCanvas.gameObject.SetActive(true);
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

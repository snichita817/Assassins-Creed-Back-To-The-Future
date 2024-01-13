using UnityEngine;

public class NextSceneScript : MonoBehaviour
{
    // Reference to the Colorblind script attached to the camera
    public Wilberforce.Colorblind colorblindScript;

    void Start()
    {
        // Check if the ColorblindType key exists in PlayerPrefs
        if (PlayerPrefs.HasKey("ColorblindType"))
        {
            // Retrieve the colorblind type from PlayerPrefs
            int colorblindType = PlayerPrefs.GetInt("ColorblindType");

            // Set the colorblind type in the Colorblind script
            if (colorblindScript != null)
            {
                colorblindScript.Type = colorblindType;
            }
        
        }
    }
}
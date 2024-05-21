using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustAttackImageSize : MonoBehaviour
{
    private RawImage rawImage;
    private RectTransform rectTransform;

    [Range(0.0f, 1.0f)]
    public float fillPercentage = 1.0f; // Control the percentage of the screen the image should fill

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        rectTransform = rawImage.GetComponent<RectTransform>();
        AdjustSize();
    }

    void Update()
    {
        AdjustSize();
    }

    private void AdjustSize()
    {
        if (rawImage.texture == null)
        {
            Debug.LogWarning("RawImage texture is missing. Please assign a texture.");
            return;
        }

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float screenAspectRatio = screenWidth / screenHeight;

        float textureWidth = rawImage.texture.width;
        float textureHeight = rawImage.texture.height;
        float textureAspectRatio = textureWidth / textureHeight;

        float newWidth, newHeight;

        if (screenAspectRatio > textureAspectRatio)
        {
            float scaleFactor = (screenWidth / textureWidth) * fillPercentage;
            newWidth = screenWidth * fillPercentage;
            newHeight = textureHeight * scaleFactor;
        }
        else
        {
            float scaleFactor = (screenHeight / textureHeight) * fillPercentage;
            newWidth = textureWidth * scaleFactor;
            newHeight = screenHeight * fillPercentage;
        }

        rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero;
    }
}

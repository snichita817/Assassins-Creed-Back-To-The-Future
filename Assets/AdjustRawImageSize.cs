using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;


public class AdjustRawImageSize : MonoBehaviour
{
    private RawImage rawImage;
    private RectTransform rectTransform;
    public Transform player;
    public GameObject VideoPlayer;
    private InputManager inputManager;
    private GameObject background;
    public GameObject SaveLoader;
    public GameObject AttackAnimation;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        rectTransform = rawImage.GetComponent<RectTransform>();
        if (SaveGame.Load<bool>("cutscene", false))
        {
            SetInactive();
            return;
        }
        else
        {
            Invoke("SetInactive", 76f);
        }

        AdjustSize();
        CreateBackground();
        inputManager = player.GetComponent<InputManager>();
        inputManager.enabled = false;
    }

    void Update()
    {
        AdjustSize();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelInvoke("SetInactive");
            SetInactive();
        }
    }

    private void AdjustSize()
    {
        float screenAspectRatio = (float)Screen.width / Screen.height;

        float textureAspectRatio = (float)rawImage.texture.width / rawImage.texture.height;

        float newHeight, newWidth;

        if (textureAspectRatio >= screenAspectRatio)
        {
            newWidth = Screen.width;
            newHeight = newWidth / textureAspectRatio;
        }
        else
        {
            newHeight = Screen.height;
            newWidth = newHeight * textureAspectRatio;
        }

        rectTransform.sizeDelta = new Vector2(newWidth, newHeight);

        rectTransform.anchoredPosition = Vector2.zero;
    }

    private void CreateBackground()
    {
        background = new GameObject("CutsceneBackground");
        background.transform.SetParent(transform.parent);

        RawImage bgRawImage = background.AddComponent<RawImage>();
        bgRawImage.color = Color.black;

        RectTransform bgRect = background.GetComponent<RectTransform>();
        bgRect.anchorMin = Vector2.zero;
        bgRect.anchorMax = Vector2.one;
        bgRect.pivot = new Vector2(0.5f, 0.5f);
        bgRect.sizeDelta = Vector2.zero;
        bgRect.anchoredPosition = Vector2.zero;

        bgRawImage.transform.SetAsFirstSibling();
    }

    private void SetInactive()
    {
        AttackAnimation.SetActive(true);
        if (inputManager == null)
        {
            inputManager = player.GetComponent<InputManager>();
        }
        //inputManager.enabled = true;
        SaveLoader.GetComponent<LoadSaveScript>().StartC();

        if (background != null)
        {
            Destroy(background);
        }

        SaveGame.Save<bool>("cutscene", true);
        VideoPlayer.SetActive(false);
        gameObject.SetActive(false);
    }
}

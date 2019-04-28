using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // All the sprites for the hearts based on your health
    public Sprite Heart1_1;
    public Sprite Heart1_2;
    public Sprite Heart2_1;
    public Sprite Heart2_2;
    public Sprite Heart3_1;
    public Sprite Heart3_2;
    public Sprite Heart4_1;
    public Sprite Heart4_2;
    public Sprite Heart5_1;
    public Sprite Heart5_2;
    public float HeartScale = 0.8f;

    private HorseController horseController;

    private Image image;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject Horse = GameObject.Find("Horse");
        HorseController horseController = Horse.GetComponent<HorseController>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (horseController == null)
        {
            GameObject Horse = GameObject.Find("Horse");
            horseController = Horse?.GetComponent<HorseController>();

            image.enabled = false;
            if (horseController == null)
            {
                return;
            }
        }
        else
        {
            image.enabled = true;
        }

        Vector3 HeartDrawScale = new Vector3(HeartScale * horseController.currentLives, HeartScale, HeartScale);
        RectTransform recttransform = GetComponent<RectTransform>();
        recttransform.localScale = HeartDrawScale;

        if (horseController.currentLives >= 5)
        {
            image.sprite = Heart5_1;
        }
        else if (horseController.currentLives == 4)
        {
            image.sprite = Heart4_1;
        }
        else if (horseController.currentLives == 3)
        {
            image.sprite = Heart3_1;
        }
        else if (horseController.currentLives == 2)
        {
            image.sprite = Heart2_1;
        }
        else if (horseController.currentLives == 1)
        {
            image.sprite = Heart1_1;
        }
    }
}

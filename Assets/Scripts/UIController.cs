using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // All the sprites for the hearts based on your health
    public Sprite Heart_1_1;
    public Sprite Heart_1_2;
    public Sprite Heart_2_1;
    public Sprite Heart_2_2;
    public Sprite Heart_3_1;
    public Sprite Heart_3_2;
    public Sprite Heart_4_1;
    public Sprite Heart_4_2;
    public Sprite Heart_5_1;
    public Sprite Heart_5_2;

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

        Vector3 HeartScale = new Vector3(0.8f * horseController.currentLives, 0.8f, 0.8f);
        RectTransform recttransform = GetComponent<RectTransform>();
        recttransform.localScale = HeartScale;

        if (horseController.currentLives >= 5)
        {
            image.sprite = Heart_5_1;
        }
        else if (horseController.currentLives == 4)
        {
            image.sprite = Heart_4_1;
        }
        else if (horseController.currentLives == 3)
        {
            image.sprite = Heart_3_1;
        }
        else if (horseController.currentLives == 2)
        {
            image.sprite = Heart_2_1;
        }
        else if (horseController.currentLives == 1)
        {
            image.sprite = Heart_1_1;
        }
    }
}

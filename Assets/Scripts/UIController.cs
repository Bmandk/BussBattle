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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject Horse = GameObject.Find("Horse");
        HorseController HorseController = Horse.GetComponent<HorseController>();
        Vector3 HeartScale = new Vector3(1f * HorseController.currentLives, 1f, 1f);

        if (HorseController.currentLives >= 5)
        {
            RectTransform recttransform = GetComponent<RectTransform>();
            recttransform.localScale = HeartScale;
            Image image = GetComponent<Image>();
            image.sprite = Heart_5_1;
        }
        else if (HorseController.currentLives == 4)
        {
            RectTransform recttransform = GetComponent<RectTransform>();
            recttransform.localScale = HeartScale;
            Image image = GetComponent<Image>();
            image.sprite = Heart_4_1;
        }
        else if (HorseController.currentLives == 3)
        {
            RectTransform recttransform = GetComponent<RectTransform>();
            recttransform.localScale = HeartScale;
            Image image = GetComponent<Image>();
            image.sprite = Heart_3_1;
        }
        else if (HorseController.currentLives == 2)
        {
            RectTransform recttransform = GetComponent<RectTransform>();
            recttransform.localScale = HeartScale;
            Image image = GetComponent<Image>();
            image.sprite = Heart_2_1;
        }
        else if (HorseController.currentLives == 1)
        {
            RectTransform recttransform = GetComponent<RectTransform>();
            recttransform.localScale = HeartScale;
            Image image = GetComponent<Image>();
            image.sprite = Heart_1_1;
        }
    }
}

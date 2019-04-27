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
        GameObject Horse = GameObject.Find("Horse");
        PlayerScript HorseController = Horse.GetComponent<HorseController>();
        if (HorseController == 5)
        {
            PlayerScript Image = self.GetComponent<Image>();
            Image.SourceImage = Heart_5_1;
        }
        else if (HorseController == 4)
        {
            PlayerScript Image = self.GetComponent<Image>();
            Image.SourceImage = Heart_4_1;
        }
        else if (HorseController == 3)
        {
            PlayerScript Image = self.GetComponent<Image>();
            Image.SourceImage = Heart_3_1;
        }
        else if (HorseController == 2)
        {
            PlayerScript Image = self.GetComponent<Image>();
            Image.SourceImage = Heart_2_1;
        }
        else if (HorseController == 1)
        {
            PlayerScript Image = self.GetComponent<Image>();
            Image.SourceImage = Heart_1_1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

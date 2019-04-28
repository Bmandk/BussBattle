using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startgame : MonoBehaviour
{
    private bool startPressed;
    private bool nomorePress;
    private Animation anim;
    private float starttimer;
    private bool startwaspressed;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount > 600) { 
        nomorePress = false;
        if (anim.isPlaying)
        {
            nomorePress = true;
        }
        if (Input.anyKey && nomorePress == false) {
            startPressed = true;
            startwaspressed = true;
        }
            if (startPressed == true && nomorePress == false)
            {
                anim.Play("paper 1 anim return");
                anim.Play("paper 2 anim return");
                anim.Play("paper 3 anim return");
                anim.Play("paper 4 anim return");
                anim.Play("paper 5 anim return");
                startPressed = false;
                nomorePress = true;
            }
           if (startwaspressed == true) {
                starttimer++;
            }
           if (starttimer.Equals(30))
            {
                SceneManager.LoadScene("JonathanScene");
            }
        }
    }
}

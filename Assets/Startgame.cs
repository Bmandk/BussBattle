using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startgame : MonoBehaviour
{
    private bool startPressed;
    private bool nomorePress;
    private Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.isPlaying)
        {
            return; 
        }
        if (Input.anyKey && nomorePress == false) {
            startPressed = true;
        }
        if (startPressed == true && nomorePress == false) {
            anim.Play("paper 1 anim return");
            anim.Play("paper 2 anim return");
            anim.Play("paper 3 anim return");
            anim.Play("paper 4 anim return");
            anim.Play("paper 5 anim return");
            anim.Play("paper 6 anim return");
            anim.Play("paper 7 anim return");
            anim.Play("paper 8 anim return");
            startPressed = false;
            nomorePress = true;
        }
    }
}

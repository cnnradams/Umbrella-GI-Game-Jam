using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{

    public float StartingTimeLeft = 60;
    public float timeLeft;
    public Text timer;


    void Start()
    {
        timeLeft = StartingTimeLeft;
    }
   

    void Update()
    {
        timeLeft -= Time.deltaTime;

        // Here we can add time per Friend saved!
        // Maybe something like:
        // if (player saved){
        //   timeLeft += 10f; }


        timer.text = timeLeft.ToString("F");

        // When timer ends, here we make it so the player cannot move,
        // and  game restarts on a button press (?)
        if (timeLeft <= 0.0f)
        {
            timer.text = "";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{

    private GameObject gameManager;
    public float StartingTimeLeft = 60;
    public float timeLeft;
    public Text timer;


    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        timeLeft = StartingTimeLeft;
    }
   

    void Update()
    {
        timeLeft -= Time.deltaTime;


        timer.text = timeLeft.ToString("F");

        // END GAME
        if (timeLeft <= 0.0f)
        {
            timer.text = "GAME OVER";
        }

    }
}
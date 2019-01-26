using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{

    public float StartingTimeLeft = 60;
    private float timeLeft;
    public Text timer;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = StartingTimeLeft;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        timer.text = timeLeft.ToString("F");

        if (timeLeft <= 0.0f)
        {
            timer.text = "GAME OVER";
        }
    }
}

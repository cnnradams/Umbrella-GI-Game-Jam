using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float StartingTimeLeft = 30;
    public float timeLeft;
    public Text timer;

    GameManager g;
    void Start()
    {
        timeLeft = StartingTimeLeft;
        g = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }


    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (!gameOver)
        {
            timer.text = timeLeft.ToString("F");
        }


        // END GAME
        if (timeLeft <= 0.0f)
        {
            GameOver();
        }

    }
    bool gameOver = false;
    public void GameOver()
    {
        gameOver = true;
        timer.text = "Game Over. Your Score: " + g.people_saved;
        Invoke("Menu", 3f);
    }
    void Menu()
    {
        SceneManager.LoadScene(1);
    }
}
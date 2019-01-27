using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private Timer time;
    public int people_saved = 0;
    public bool gameOver = false;

    public GameObject player;
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            time = player.GetComponentInChildren<Canvas>().GetComponentInChildren<Timer>();
        }
	}
    public void droppedHome()
    {
        people_saved++;
        //Debug.Log("People Saved: " + people_saved);

    }
    public void addTime()
    {
        time.timeLeft += 10f;
    }
}

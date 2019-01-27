using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public float time = 60;
    public int people_saved = 0;
    public float warmth_max = 100;
    public float warmth_decrease = 10;
    public float warmth_gain = 10;

    public bool gameOver = false;

    public GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        SoundManager.instance.PlayMusic();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void droppedHome()
    {
        people_saved++;
        
    }
}

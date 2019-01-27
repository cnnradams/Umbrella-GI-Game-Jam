using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HomeWarmth : MonoBehaviour
{
    public float startingWarmth = 10f;
    private float totalWarmth;
    private PlayerWarmth playerwarmth;

    // Start is called before the first frame update
    void Start()
    {
        playerwarmth = GetComponent<PlayerWarmth>();
        totalWarmth = startingWarmth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "TestPlayer" &&
            playerwarmth.playerWarmth < playerwarmth.StartingPlayerWarmth)
        {
            Debug.Log("colliding!");
            playerwarmth.playerWarmth += totalWarmth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

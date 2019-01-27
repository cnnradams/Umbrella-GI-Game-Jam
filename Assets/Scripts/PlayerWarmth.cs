using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWarmth : MonoBehaviour
{
    // Starting values for Total Warmth and Cold damage:
    private float StartingColdDmg = 0.5f;
    public float StartingPlayerWarmth = 100;

    public float playerWarmth;
    public float coldDmg;

    public Slider warmthSlider;

    
    void Start()
    {
        playerWarmth = StartingPlayerWarmth;
        coldDmg = StartingColdDmg;
    }

    void Update()
    {
        playerWarmth -= coldDmg * Time.deltaTime;

        // Here we can add warmth if we have Friends close to us!
        // We can also increase the Cold damage here.

        warmthSlider.value = playerWarmth;
    }
}

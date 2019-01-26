using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWarmth : MonoBehaviour
{
    private float StartingColdDmg = 0.5f;
    public float StartingPlayerWarmth = 100;

    public float playerWarmth;
    public float coldDmg;

    public Slider warmthSlider;

    // Start is called before the first frame update
    void Start()
    {
        playerWarmth = StartingPlayerWarmth;
        coldDmg = StartingColdDmg;
    }

    // Update is called once per frame
    void Update()
    {
        playerWarmth -= coldDmg * Time.deltaTime;
        warmthSlider.value = playerWarmth;
        //Debug.Log(playerWarmth.ToString());
    }
}

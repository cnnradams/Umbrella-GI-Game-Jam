using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerWarmth : MonoBehaviour
{
    private Timer timer;
    public TestPlayer player;

    // Starting values for Total Warmth and Cold damage:
    public float StartingColdDmg = 1f;
    public float StartingPlayerWarmth = 100;

    public float maxPlayerWarmth;
    public float playerWarmth;
    public float coldDmg;
    private float lastColdDmg;

    public Slider warmthSlider;
   
    public float numberFriends = 0f;
    private float newNumberFriends;

    void Start()
    {
        timer = gameObject.GetComponentInChildren<Timer>();
        player = GetComponent<TestPlayer>();

        maxPlayerWarmth = StartingPlayerWarmth;
        playerWarmth = StartingPlayerWarmth;
        coldDmg = StartingColdDmg;
        lastColdDmg = coldDmg;

        newNumberFriends = numberFriends;
    }


    void Update()
    {
        numberFriends = (player.friend_list_length);

        lastColdDmg = coldDmg;
        if (numberFriends > 0)
        {
            if (numberFriends > newNumberFriends)
            {
                coldDmg -= numberFriends/15;
                newNumberFriends = numberFriends;
            }
            else if (numberFriends < newNumberFriends)
            {
                coldDmg += numberFriends/15;
                newNumberFriends = numberFriends;
            }
        }
        else
        {
            coldDmg = lastColdDmg;
        }
        
        if (playerWarmth <= maxPlayerWarmth)
        {
            playerWarmth -= coldDmg * Time.deltaTime;
        }
        else
        {
            playerWarmth = maxPlayerWarmth;
        }

        coldDmg += 0.01f * Time.deltaTime;
        

        warmthSlider.value = playerWarmth;
    }
}

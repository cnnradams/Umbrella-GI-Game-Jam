using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// private Timer timer;
// timer = gameObject.GetComponentInChildren<Timer>();


public class PlayerWarmth : MonoBehaviour
{

    public TestPlayer player;

    // Starting values for Total Warmth and Cold damage:
    public float StartingColdDmg = 1f;
    public float StartingPlayerWarmth = 100;

    public float maxPlayerWarmth;
    public float playerWarmth;
    private float coldDmg;

    public Slider warmthSlider;
   
    public float numberFriends = 0f;
    private float newNumberFriends;

    void Start()
    {
        player = GetComponent<TestPlayer>();

        maxPlayerWarmth = StartingPlayerWarmth;
        playerWarmth = StartingPlayerWarmth;
        coldDmg = StartingColdDmg;

        newNumberFriends = numberFriends;
    }


    void Update()
    {
        numberFriends = (player.friend_list_length);
        
       

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
            coldDmg = StartingColdDmg;
        }
        
        if (playerWarmth <= maxPlayerWarmth)
        {
            playerWarmth -= coldDmg * Time.deltaTime;
        }
        else
        {
            playerWarmth = maxPlayerWarmth;
        }
        

        // We can also increase the Cold damage here.

        warmthSlider.value = playerWarmth;
    }
}

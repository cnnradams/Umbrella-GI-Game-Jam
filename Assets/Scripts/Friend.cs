using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour
{
    public bool cold = true;

    private Rigidbody2D rb;
    private Animator anim;
    private GameObject player;

    private float offsetPosition;
    private float offsetExit = 1f;
    // Start is called before the first frame update
    void Start()
    {
        cold = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Cold", cold);
        if (!cold)
        {
            followPlayer();
        }
    }

    public void pickup()
    {
        cold = false;
        //Change Sprite
        //Start Following Player
        offsetPosition = Random.RandomRange(-1f, 1f);
        gameObject.GetComponent<Rigidbody2D>().isKinematic= true;
        //Change Physics to Kinematic
    }
    public void drop()
    {
        cold = true;
        //Don't move
        float direction = player.GetComponent<Rigidbody2D>().velocity.x;
        if(direction > 0)
        {
            gameObject.transform.position = player.transform.position + new Vector3(-offsetExit, 0);
        }
        else
        {
            gameObject.transform.position = player.transform.position + new Vector3(offsetExit, 0);
        }
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        //Retain Pyhsical Properties
    }
    private void followPlayer()
    {
        gameObject.transform.position = player.transform.position + new Vector3(offsetPosition,0);
        
    }

}

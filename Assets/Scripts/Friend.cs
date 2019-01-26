using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour
{
    public bool cold = true;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        cold = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Cold", cold);
    }

    public void pickup()
    {
        cold = false;
        //Change Sprite
        //Start Following Player
    }
    public void drop()
    {
        cold = true;
    }
}

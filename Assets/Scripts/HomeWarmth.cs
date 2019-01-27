using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HomeWarmth : MonoBehaviour
{
    public float regen_warmth = 5f;
    private PlayerWarmth playerwarmth;
    private GameManager gameManager;
    public float radius_scale = 0.5f;
    public CircleCollider2D circle2D;
    public bool inHouse;
    public Light light;
    public float intensity_scale;
    // Start is called before the first frame update
    void Start()
    {
        playerwarmth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWarmth>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            inHouse = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inHouse = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        circle2D.radius = gameManager.people_saved * radius_scale + 1f;
        light.range = gameManager.people_saved * radius_scale * 10f + 10f;
        light.intensity = gameManager.people_saved * intensity_scale + 10f;
        if (inHouse)
        {
            playerwarmth.playerWarmth += regen_warmth * Time.deltaTime;
        }
    }
}

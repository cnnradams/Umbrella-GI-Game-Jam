using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    GameObject playerInstance;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnPlayer()
    {
        playerInstance = (GameObject)Instantiate(playerPrefab, transform.position, Quaternion.identity);
    }
}

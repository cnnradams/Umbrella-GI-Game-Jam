using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public GameObject groundPrefab;
    public GameObject friendPrefab;

    public float heightFromBottom = 2;
    List<Platform> platforms;
    // pass in from calling class
    public float width;
    public int index;

    public int minFriendsPerChunk = 0;
    public int maxFriendsPerChunk = 2;

    GameObject instantiatedGround;
    // Start is called before the first frame update
    void Start()
    {
        platforms = new List<Platform>();
        instantiatedGround = Instantiate(groundPrefab, transform.position, Quaternion.identity, transform);
        instantiatedGround.transform.localScale = new Vector3(width, instantiatedGround.transform.localScale.y, instantiatedGround.transform.localScale.z);
        platforms.Add(new Platform(instantiatedGround, instantiatedGround.transform.position.y + instantiatedGround.transform.localScale.y / 2.0f));

        // Generating Friends in chunk
        int numFriends = Random.Range(minFriendsPerChunk, maxFriendsPerChunk + 1);
        for (int i = 0; i < numFriends; i++)
        {
            InstantiateFriend();
        }
    }
    void InstantiateFriend()
    {
        float yPos = platforms[Random.Range(0, platforms.Count)].topValue + friendPrefab.transform.localScale.y / 2.0f;
        Instantiate(friendPrefab, new Vector2(Random.Range(transform.position.x - width / 2.0f, transform.position.x + width / 2.0f), yPos),
        Quaternion.identity, transform);
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void Generate()
    {

    }
    class Platform
    {
        public GameObject platformObj;
        public float topValue;
        public Platform(GameObject platformObj, float topValue)
        {
            this.platformObj = platformObj;
            this.topValue = topValue;
        }
    }
}

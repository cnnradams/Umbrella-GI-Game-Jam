using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public GameObject groundPrefab;
    public GameObject friendPrefab;
    public GameObject platformPrefab;

    public float platformGenChance = 0.5f;
    public float minPlatformHeightDifference = 1;
    public float maxPlatformHeightDifference = 10;
    public float minPlatformSize = 5;
    List<Platform> platforms;
    // pass in from calling class
    public float width;
    public int index;
    public List<Platform> previousPlatforms;
    public bool isLeft = false;

    public int minFriendsPerChunk = 0;
    public int maxFriendsPerChunk = 2;
    Platform groundPlatform;
    GameObject instantiatedGround;
    // Start is called before the first frame update
    void Start()
    {
        platforms = new List<Platform>();
        instantiatedGround = Instantiate(groundPrefab, new Vector2(transform.position.x, transform.position.y + (index == 0 || index == -1 ? 0 : Random.Range(0, 5f))), Quaternion.identity, transform);
        instantiatedGround.transform.localScale = new Vector3(width, instantiatedGround.transform.localScale.y, instantiatedGround.transform.localScale.z);
        groundPlatform = new Platform(instantiatedGround, instantiatedGround.transform.position.y + instantiatedGround.transform.localScale.y / 2.0f);
        GeneratePlatforms(previousPlatforms, isLeft);
        // Generating Friends in chunk
        int numFriends = Random.Range(minFriendsPerChunk, maxFriendsPerChunk + 1);
        for (int i = 0; i < numFriends; i++)
        {
            InstantiateFriend();
        }

    }
    void InstantiateFriend()
    {
        int range = Random.Range(0, platforms.Count + 1);
        Transform tr;
        float yPos;
        if (range == platforms.Count)
        {
            yPos = groundPlatform.topValue + friendPrefab.transform.localScale.y / 2.0f;
            tr = groundPlatform.platformObj.transform;
        }
        else
        {
            yPos = platforms[range].topValue + friendPrefab.transform.localScale.y / 2.0f;
            tr = platforms[range].platformObj.transform;
        }
        Instantiate(friendPrefab, new Vector2(Random.Range(tr.position.x - tr.localScale.x / 2.0f, tr.position.x + tr.localScale.x / 2.0f), yPos),
        Quaternion.identity, transform);
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void GeneratePlatforms(List<Platform> heights, bool isLeft)
    {
        foreach (Platform p in heights)
        {
            float w = Random.Range(minPlatformSize, width / 1.5f);
            if (w > width)
            {
                w = width;
            }
            float xPos;
            if (isLeft)
            {
                xPos = transform.position.x + width / 2.0f - w / 2.0f;
            }
            else
            {
                xPos = transform.position.x - width / 2.0f + w / 2.0f;
            }
            GameObject newP = Instantiate(platformPrefab, new Vector2(xPos, p.platformObj.transform.position.y), Quaternion.identity, transform);
            newP.transform.localScale = new Vector3(w, newP.transform.localScale.y, 1);
            Platform n = new Platform(newP, newP.transform.position.y + newP.transform.localScale.y / 2.0f);
            platforms.Add(n);
        }
        GenerateNewPlatforms();
    }
    void GenerateNewPlatforms()
    {
        float curYHeight = instantiatedGround.transform.position.y + instantiatedGround.transform.localScale.y / 2.0f + 2;
        // keep making new platforms until luck runs out
        while (Random.Range(0.0f, 1.0f) < platformGenChance)
        {
            bool good = true;
            do
            {
                good = true;
                curYHeight += Random.Range(0, maxPlatformHeightDifference);
                foreach (Platform p in platforms)
                {
                    if (Mathf.Abs(p.platformObj.transform.position.y - curYHeight) < minPlatformHeightDifference)
                    {
                        good = false;
                    }
                }
            } while (!good);
            float xPos = transform.position.x + Random.Range(-width / 3.0f, width / 3.0f);
            float xWidth = Random.Range(minPlatformSize, width);
            if (xPos < transform.position.x)
            {
                if (xPos - xWidth / 2.0f < transform.position.x - width / 2.0f)
                {
                    xWidth = (xPos - (transform.position.x - width / 2.0f)) * 2.0f;
                }
            }
            else
            {
                if (xPos + xWidth / 2.0f > transform.position.x + width / 2.0f)
                {
                    xWidth = ((transform.position.x + width / 2.0f) - xPos) * 2.0f;
                }
            }
            GameObject plat = Instantiate(platformPrefab, new Vector2(xPos, curYHeight), Quaternion.identity, transform);
            plat.transform.localScale = new Vector3(xWidth, plat.transform.localScale.y, 1);
            platforms.Add(new Platform(plat, plat.transform.position.y + plat.transform.localScale.y / 2.0f));
        }
    }
    public List<Platform> GetRightPlatformPoints(bool isLeft)
    {
        List<Platform> rightPlatforms = new List<Platform>();
        foreach (Platform p in platforms)
        {
            if (isLeft)
            {
                if (p.platformObj.transform.position.x - p.platformObj.transform.localScale.x / 2.0f <=
                    transform.position.x - width / 2.0f)
                {
                    rightPlatforms.Add(p);
                }
            }
            else
            {
                if (p.platformObj.transform.position.x + p.platformObj.transform.localScale.x / 2.0f >=
                    transform.position.x + width / 2.0f)
                {
                    rightPlatforms.Add(p);
                }
            }
        }
        return rightPlatforms;
    }
    public class Platform
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

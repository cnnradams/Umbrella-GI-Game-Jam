using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public int chunkWidthMin = 8;
    public int chunkWidthMax = 32;
    public Chunk chunkPrefab;
    int generateOffset = 20;
    public Camera tCamera;
    // Start is called before the first frame update
    void Start()
    {
        chunks.Add(0, GenerateChunk(0, false));
    }
    public Dictionary<int, Chunk> chunks = new Dictionary<int, Chunk>();
    int currentFurthestRightChunk = 0;
    int currentFurthestLeftChunk = 0;

    float furthestRightPosition = 0;
    float furthestLeftPosition = 0;

    // Update is called once per frame
    void Update()
    {
        if (!tCamera)
        {
            tCamera = GameObject.FindGameObjectWithTag("Player").transform.GetComponentInChildren<Camera>();
        }
        // Right chunk generation
        float cameraSize = tCamera.orthographicSize;

        float furthestCameraRight = tCamera.transform.position.x + cameraSize;
        if (furthestRightPosition - furthestCameraRight < generateOffset)
        {
            ++currentFurthestRightChunk;
            chunks.Add(currentFurthestRightChunk, GenerateChunk(currentFurthestRightChunk, false));
        }

        // Left chunk genereation
        float furthestCameraLeft = tCamera.transform.position.x - cameraSize;
        if (Mathf.Abs(furthestLeftPosition - furthestCameraLeft) < generateOffset)
        {
            --currentFurthestLeftChunk;
            chunks.Add(currentFurthestLeftChunk, GenerateChunk(currentFurthestLeftChunk, true));
        }
    }

    // -1 down, 0 normal, 1 up
    int randomUp = 0;
    public float randomUpChance = 0.2f;
    Chunk GenerateChunk(int index, bool isLeft)
    {
        int chunkWidth = Random.Range(chunkWidthMin, chunkWidthMax);
        if (index == 0 || index == -1)
        {
            chunkWidth = 32;
        }
        Chunk c = Instantiate(chunkPrefab, new Vector3((isLeft ? furthestLeftPosition - chunkWidth / 2.0f : furthestRightPosition + chunkWidth / 2.0f), 0, 0), Quaternion.identity, transform) as Chunk;
        c.width = chunkWidth;
        c.index = index;
        if (isLeft)
        {
            furthestLeftPosition = c.transform.position.x - chunkWidth / 2.0f;
        }
        else
        {
            furthestRightPosition = c.transform.position.x + chunkWidth / 2.0f;
        }
        Chunk last;
        float rand = Random.Range(0f, 1f);
        //Debug.Log(rand);
        //Debug.Log(rand < randomUpChance);
        if (rand < randomUpChance)
        {
            randomUp = Random.Range(-1, 2);
            //Debug.Log("New: " + randomUp);
        }
        c.randomDir = randomUp;
        chunks.TryGetValue(index + (isLeft ? 1 : -1), out last);
        c.isLeft = isLeft;
        if (last == null)
        {
            //Debug.Log("no last height");
            c.previousPlatforms = new List<Chunk.Platform>();
            c.lastHeight = 0;
        }
        else
        {
            c.previousPlatforms = last.GetRightPlatformPoints(isLeft);
            c.lastHeight = last.newHeight;
        }

        return c;
    }
}

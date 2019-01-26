using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public int chunkWidth = 32;
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
        float furthestGeneratedRight = (currentFurthestRightChunk * chunkWidth) + (chunkWidth / 2.0f);
        if (furthestGeneratedRight - furthestCameraRight < generateOffset)
        {
            ++currentFurthestRightChunk;
            chunks.Add(currentFurthestRightChunk, GenerateChunk(currentFurthestRightChunk, false));
        }

        // Left chunk genereation
        float furthestCameraLeft = tCamera.transform.position.x - cameraSize;
        float furthestGeneratedLeft = (currentFurthestLeftChunk * chunkWidth) - (chunkWidth / 2.0f);
        if (Mathf.Abs(furthestGeneratedLeft - furthestCameraLeft) < generateOffset)
        {
            --currentFurthestLeftChunk;
            chunks.Add(currentFurthestLeftChunk, GenerateChunk(currentFurthestLeftChunk, true));
        }
    }
    Chunk GenerateChunk(int index, bool isLeft)
    {
        Chunk c = Instantiate(chunkPrefab, new Vector3(index * chunkWidth, 0, 0), Quaternion.identity, transform) as Chunk;
        c.width = chunkWidth;
        c.index = index;
        Chunk last;
        chunks.TryGetValue(index + (isLeft ? 1 : -1), out last);
        c.isLeft = isLeft;
        if (last == null)
        {
            c.previousPlatforms = new List<Chunk.Platform>();
        }
        else
        {
            c.previousPlatforms = last.GetRightPlatformPoints(isLeft);
        }

        return c;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public int chunkWidth = 32;
    public Chunk chunkPrefab;
    int generateOffset = 20;
    // Start is called before the first frame update
    void Start()
    {
        chunks.Add(0, GenerateChunk(0));
    }
    public Dictionary<int, Chunk> chunks = new Dictionary<int, Chunk>();
    int currentFurthestRightChunk = 0;
    int currentFurthestLeftChunk = 0;
    // Update is called once per frame
    void Update()
    {
        // Right chunk generation
        float cameraSize = Camera.main.orthographicSize;

        float furthestCameraRight = Camera.main.transform.position.x + cameraSize;
        float furthestGeneratedRight = (currentFurthestRightChunk * chunkWidth) + (chunkWidth / 2.0f);
        if (furthestGeneratedRight - furthestCameraRight < generateOffset)
        {
            ++currentFurthestRightChunk;
            chunks.Add(currentFurthestRightChunk, GenerateChunk(currentFurthestRightChunk));
        }

        // Left chunk genereation
        float furthestCameraLeft = Camera.main.transform.position.x - cameraSize;
        float furthestGeneratedLeft = (currentFurthestLeftChunk * chunkWidth) - (chunkWidth / 2.0f);
        if (Mathf.Abs(furthestGeneratedLeft - furthestCameraLeft) < generateOffset)
        {
            --currentFurthestLeftChunk;
            chunks.Add(currentFurthestLeftChunk, GenerateChunk(currentFurthestLeftChunk));
        }
    }
    Chunk GenerateChunk(int index)
    {
        Chunk c = Instantiate(chunkPrefab, new Vector3(index * chunkWidth, 0, 0), Quaternion.identity, transform) as Chunk;
        c.width = chunkWidth;
        c.index = index;
        return c;
    }
}

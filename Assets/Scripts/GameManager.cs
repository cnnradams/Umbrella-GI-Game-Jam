using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour {
    public Tilemap DarkMap;
    public Tilemap BlurMap;
    public Tilemap BackgroundMap;

    public float time = 60;
    public int people_saved = 0;
    public float warmth_max = 100;
    public float warmth_decrease = 10;
    public float warmth_gain = 10;

    public bool gameOver = false;

    public Tile DarkTile;
    public Tile BlurredTile;

    public GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        DarkMap.origin = BlurMap.origin = BackgroundMap.origin;
        DarkMap.size = BlurMap.size = BackgroundMap.size;

        foreach(Vector3Int p in DarkMap.cellBounds.allPositionsWithin)
        {
            DarkMap.SetTile(p, DarkTile);
        }
        foreach (Vector3Int p in BlurMap.cellBounds.allPositionsWithin)
        {
            BlurMap.SetTile(p, BlurredTile);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void friendDropped()
    {
        people_saved++;
        warmth_max += warmth_gain;
    }
}

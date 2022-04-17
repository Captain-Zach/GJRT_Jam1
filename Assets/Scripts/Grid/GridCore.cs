using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridCore : MonoBehaviour
{
    private enum prospective {
        None,
        WaterStream
    }
    [SerializeField] int Width;
    [SerializeField] int Height;

    [SerializeField] int refreshDelay;

    [SerializeField] GameObject BGTile;

    // private float gridSpacing = 1;
    // Toggle these for testing
    public float gridSpacing = 20;


   public List<List<GameObject>> gridMap = new List<List<GameObject>>();

    
   // Instead of listing lists of game objects, why don't we... Create a tile class
   // and keep a nested list of that? We could put anything in it that inherits from it. 

    /* 
        Imagine something like this when given a map with a size of 4 by 4:
        [
            [0, 0, 0, 0],
            [0, 0, 0, 0],
            [0, 0, 0, 0],
            [0, 0, 0, 0],
            [0, 0, 0, 0],
            [0, 0, 0, 0]
        ]


    */

    void Start()
    {
        // Going to loop through
        for(int y = 0; y < Height; y++)
        {
            // This is a bit like building the train tracks as the train is running over it.
            gridMap.Add(new List<GameObject>());

            for(int x = 0; x < Width; x++)
            {
                // Instantiate
                // Vector2 TestBoi = new Vector2(x * gridSpacing, y * gridSpacing * -1); // Yoooo what was I thinkin'?
                GameObject Tile = Instantiate(BGTile, this.transform);
                gridMap[y].Add(Tile);

                // Tile.GetComponent<TileNode>().Init(x, y, TileLevelInterpreter.TileTypes.None);

            }
        }

        // Debug.Log(gridMap);
        // Debug.Log(gridMap[0][0]);
    }

    // Update is called once per frame
    void Update()
    {
        dynaSpacing();
        
    }

    void dynaSpacing()
    {
        // This was for test purposes, but serves as a fine test.
        int xTracker = 0;
        int yTracker = 0;
        foreach(List<GameObject> row in gridMap)
        {
            foreach (GameObject Tile in row)
            {
                Tile.transform.position = new Vector2(xTracker * gridSpacing, yTracker * gridSpacing * -1);
                xTracker++;
                // Debug.Log(xTracker + " " + yTracker);
            }
            xTracker = 0;
            yTracker++;
        }
    }

    // Need a function to instantiate new Tiles
    void pushTile(int xCoord, int yCoord, TileLevelInterpreter.TileTypes tileType) // need x, y coordinates for where tile pushes and the tiletype
    {
        // GameObject newTilePrefab = GetPrefab(tileType);
    }

    // Function to destroy a Tile
    void removeTile(int xCoord, int yCoord) // targetted by coordinates
    {
        // gridMap[y][x] = 
    }


    // Function to swap a tile
    void swapTile(int xCoord, int yCoord, TileLevelInterpreter.TileTypes newTileType)
    {
        // remove tile
        // removeTile(xCoord, yCoord);

        // push tile
        // pushTile(xCoord, yCoord, newTileType);        
    }
}

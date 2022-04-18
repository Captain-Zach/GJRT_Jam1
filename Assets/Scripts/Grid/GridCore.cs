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

    [SerializeField] GameObject[] TileList = {
        

    };
    [SerializeField] int Width;
    [SerializeField] int Height;

    [SerializeField] int refreshDelay;
    
    [SerializeField] GameObject Loader;


    [SerializeField] GameObject BGTile;

    // private float gridSpacing = 1;
    // Toggle these for testing
    public float gridSpacing = 20;

    // [Serialize]


   public List<List<TileNode.NodeTemplate>> gridMap = new List<List<TileNode.NodeTemplate>>();

    
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
        TileLevelInterpreter Terp = Loader.GetComponentInChildren(typeof(TileLevelInterpreter)) as TileLevelInterpreter;

        // List<List<TileTypes>> = Terp.GetGridTiles();
        // TileNode.HelloTest();

        List<List<TileLevelInterpreter.TileTypes>> gridlist = Terp.GetGridTiles();
        
        // Going to loop through
        for(int y = 0; y < gridlist.Count - 1; y++)
        {
            // This is a bit like building the train tracks as the train is running over it.
            gridMap.Add(new List<TileNode.NodeTemplate>());

            for(int x = 0; x < gridlist[0].Count; x++)
            {

                // lets use a switch to pick which sprite goes in here.
                Debug.Log(gridlist[y][x]);
                GameObject pickedPrefab;
                switch(gridlist[y][x])
                {
                    case TileLevelInterpreter.TileTypes.None:
                        pickedPrefab = TileList[0];
                        break;
                    case TileLevelInterpreter.TileTypes.Village:
                        pickedPrefab = TileList[7];
                        break;
                    case TileLevelInterpreter.TileTypes.Fork:
                        pickedPrefab = TileList[9];
                        break;
                    case TileLevelInterpreter.TileTypes.BasicBlock:
                        pickedPrefab = TileList[8];
                        break;
                    case TileLevelInterpreter.TileTypes.BeaversHouse:
                        pickedPrefab = TileList[5];
                        break;
                    case TileLevelInterpreter.TileTypes.WaterStream:
                        pickedPrefab = TileList[6];
                        break;
                    case TileLevelInterpreter.TileTypes.City:
                        pickedPrefab = TileList[7];
                        break;
                    case TileLevelInterpreter.TileTypes.CitysHitbox:
                        pickedPrefab = TileList[0];
                        break;
                    case TileLevelInterpreter.TileTypes.VillagesHitbox:
                        pickedPrefab = TileList[0];
                        break;
                    default:
                        pickedPrefab = TileList[0];
                        break;
                }
                // Instantiate
                // Vector2 TestBoi = new Vector2(x * gridSpacing, y * gridSpacing * -1); // Yoooo what was I thinkin'?
                GameObject Tile = Instantiate(pickedPrefab, this.transform);
                TileNode.NodeTemplate Node = new TileNode.NodeTemplate(0,false, TileNode.NodeTemplate.TileTypes.None, Tile);
                // Node.TilePrefab = BGTile;
                gridMap[y].Add(Node);

                // Tile.GetComponent<TileNode>().Init(x, y, TileLevelInterpreter.TileTypes.None);

            }
        }

        // // Debug.Log(gridMap);
        // // Debug.Log(gridMap[0][0]);
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
        foreach(List<TileNode.NodeTemplate> row in gridMap)
        {
            foreach (TileNode.NodeTemplate Tile in row)
            {
                // // Debug.Log("DynaSpacing: x-" + xTracker+" y-"+ yTracker);
                // // Debug.Log(Tile.TilePrefab.transform.position);
                
                Tile.TilePrefab.transform.position = new Vector3(xTracker * gridSpacing, yTracker * gridSpacing * -1, 0);
                xTracker++;
                // // // Debug.Log(xTracker + " " + yTracker);
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

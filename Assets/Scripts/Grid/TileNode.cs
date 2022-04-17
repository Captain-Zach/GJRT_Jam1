using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNode : MonoBehaviour
{

    public List<List<GameObject>> MaskMap;
    [SerializeField] int xCoordinate = 0;
    [SerializeField] int yCoordinate = 0;
    [SerializeField] TileLevelInterpreter.TileTypes Type;
    public float Spacer = 1.0f;

    // [SerializeField] 

    


    const int width = 3;
    const int height = 3;

    [SerializeField] GameObject grass;



    public GameObject PlaceHolder;
    // This node is the 
    // Start is called before the first frame update
    void Start()
    {
        MaskMap = new List<List<GameObject>>(); // Does this look like this: [[]]?



        for(int y = 0; y < height; y++)
        {
            MaskMap.Add(new List<GameObject>());
            for(int x = 0; x < width; x++)
            {
                // This is where I need to instantiate, and change the position based on x and y values.
                GameObject newNinth = Instantiate(grass, this.transform);
                MaskMap[y].Add(newNinth); // Create

                // Let's do 2 switches to decide what their relative positions should be compared to tile.
                Vector2 coordinates = new Vector2(0,0);
                switch(y)
                {
                    case 0:
                        // uggghhhhh
                        coordinates.y = 1 * Spacer;
                        break;
                    
                    case 1:
                        coordinates.y = 0;
                        break;

                    case 2:
                        coordinates.y = -1 * Spacer;
                        break;

                    default:
                        break;
                }

                // and now we set the X coord
                switch(x)
                {
                    case 0:
                        coordinates.x = -1 * Spacer;
                        break;

                    case 1:
                        coordinates.x = 0;
                        break;

                    case 2:
                        coordinates.x = 1 * Spacer;
                        break;

                    default:
                        break;
                }

                // Finally, we can put all this info together and move our ninth.
                newNinth.transform.position = coordinates;
                Debug.Log(MaskMap[y]);
                // refreshNinths();  // Shouldn't need to pass anything 
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(int xCoord = 0, int yCoord = 0, TileLevelInterpreter.TileTypes targetType = TileLevelInterpreter.TileTypes.None)
    {
        // Need 
        this.xCoordinate = xCoord;
        this.yCoordinate = yCoord;
        this.Type = targetType;
        // Want to refresh ninths to correct composition.
        refreshNinths();
    }

    

    public void refreshNinths()
    {
        // Change MaskMap
        switch(Type)
        {
            case 0:
                gridCycle(grass);
                break;
            // case 1:
                // set this to... a basic block prefab
            // case 2:
                // set this to OneBlockWood
            // case 2:
                // set this to TwoBlockWood
            // etc
            default:
                break;
        }
        // on init, swaps, changes, etc, refresh ninths to 
    }

    void gridCycle(GameObject target)
    {
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                Debug.Log(MaskMap);
                // GameObject oldTile = MaskMap[y][x];
                // GameObject newTile = Instantiate(target, MaskMap[y][x].transform);
                // MaskMap[y][x] = newTile;
                // Destroy(oldTile);
            }
        }
    }

    void surroundCheck()
    {
        // referencing parent GridMap, 
    }

    // Longer term goals... We can dynamically choose what our tiles are like if we're willing to get a bit verbose.

    void typeNone()
    {
        // set all ninths to generic grass
    }

    void typeBasicBlock()
    {
        // set all ninths to stone
    }

    void typeWood()
    {
        // set all ninths to wood
    }

    void typeFork()
    {
        // set all ninths to grey
    }

    void typeWater() 
    {
        // set all ninths to straight water
    }

    void typeBeaverHouse()
    {
        // set all ninths to... Green, that's the code color
    }

    void typeHitbox()
    {
        // set all ninths to color code for hitbox
    }

    void typeVillage()
    {
        // set all ninths to color code for village
    }

    void typeCity()
    {
        // set all ninths to colorcode for city
    }

    public int GetXCoord() { return xCoordinate; }
    public int GetYCoord() { return yCoordinate; }
    public TileLevelInterpreter.TileTypes GetTileTypes() { return Type; }
}

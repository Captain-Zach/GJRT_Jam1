using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridCore : MonoBehaviour
{
    [SerializeField] int Width;
    [SerializeField] int Height;

    [SerializeField] float gridSpacing;
    [SerializeField] int refreshDelay;

    [SerializeField] GameObject PlaceHolder;

    // Need private nested lists to hold the changing size of the grid.
   public List<List<GameObject>> gridMap;

    /* 
        Imagine something like this when given a map with a size of 4 by 4:
        [
            [Sprite, Sprite, Sprite, Sprite],
            [Sprite, Sprite, Sprite, Sprite],
            [Sprite, Sprite, Sprite, Sprite],
            [Sprite, Sprite, Sprite, Sprite]
        ]
    */

    void Start()
    {
        for(int y = 0; y< Height; y++)
        {
            for(int x = 0; x < Width; x++)
            {
                // Instantiate
                Vector2 TestBoi = new Vector2(x * gridSpacing, y * gridSpacing);
                GameObject Tile = Instantiate(PlaceHolder, TestBoi, this.transform.rotation);

                // assign each sprite to it's place in the grid.
                // gridMap[y][x] = new GameObject();
                // gridMap[y][x] = Tile;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // dynaSpacing();
        // Debug.Log(gridMap);
    }

    void dynaSpacing()
    {
        int xTracker = 0;
        int yTracker = 0;
        foreach(List<GameObject> row in gridMap)
        {
            foreach (GameObject Tile in row)
            {
                // Tile.transform.position = new Vector2(xTracker * gridSpacing, yTracker * gridSpacing);
                xTracker++;
                Debug.Log(xTracker + " " + yTracker);
            }
            xTracker = 0;
            yTracker++;
        }
    }

}

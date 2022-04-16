using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCore : MonoBehaviour
{
    [SerializeField] int Width;
    [SerializeField] int Height;

    [SerializeField] float gridSpacing;
    [SerializeField] int refreshDelay;

    [SerializeField] GameObject PlaceHolder;

    // Need private nested lists to hold the changing size of the grid.
    private List<List<GameObject>> gridMap;

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
                Instantiate(PlaceHolder, TestBoi, this.transform.rotation);
                // assign each sprite to it's place in the grid.
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

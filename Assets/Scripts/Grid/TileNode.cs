using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNode : MonoBehaviour
{

    [SerializeField] List<List<GameObject>> MaskMap;
    public float Spacer = 1.0f;


    const int width = 3;
    const int height = 3;



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
                GameObject newNinth = Instantiate(PlaceHolder, this.transform);
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
                Debug.Log("This is a problem");
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

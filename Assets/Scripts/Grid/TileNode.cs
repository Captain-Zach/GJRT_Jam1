using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNode : MonoBehaviour
{

    [SerializeField] List<List<GameObject>> MaskMap = new List<List<GameObject>>(){};
    public GameObject PlaceHolder;
    // This node is the 
    // Start is called before the first frame update
    void Start()
    {
        for(int y = 0; y < 3; y++)
        {
            for(int x = 0; x < 3; x++)
            {
                // this.compon
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Ignore Collision between Normal layer and Water layer
        Physics2D.IgnoreLayerCollision(0, 4);
    }

    private void Update()
    {
        
    }
}

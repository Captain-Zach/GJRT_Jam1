using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    // Start is called before the first frame update
    public int xCoord;

    public int yCoord;

    public void setCoords(int xIn, int yIn){
        yCoord = yIn;
        xCoord = xIn;
    }

    void OnMouseDown()
    {
        // Destroy(this.gameObject);
        GetComponentInParent<GridCore>().nodeClickEvent(xCoord, yCoord);
    }

    void OnMouseOver()
    {
        // Debug.Log("Mouse over me "+ this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public int IndexActiveWood;
    private int TotalResources;
    [SerializeField] GameObject[] WoodResources;
    // Start is called before the first frame update
    void Start()
    {
        TotalResources = WoodResources.Length;
    }

    public GameObject GetWood()
    {
        return WoodResources[IndexActiveWood];
    }

    public GameObject GetWood(int specificIndex)
    {
        if (specificIndex > TotalResources - 1)
        {
            throw new System.Exception("There are no objects in this index in hand");
        }
        return WoodResources[specificIndex];
    }

    public int GetTotalResources()
    {
        return TotalResources;
    }

    public void UseWood()
    {
        Debug.Log(WoodResources[IndexActiveWood]);
        WoodResources[IndexActiveWood] = null;
        TotalResources--;
        GoToNextWood();
    }

    public void GoToNextWood()
    {
        if (TotalResources == 0) return;
        if (IndexActiveWood == WoodResources.Length - 1)
        {
            IndexActiveWood = 0;
        } else
        {
            IndexActiveWood++;
        }
        if (WoodResources[IndexActiveWood] == null && TotalResources != 0)
        {
            GoToNextWood();
        }
    }

    public enum WoodSizes
    {
        Wood = 1,
        WideWood = 2,
        ExtraWideWood = 3
    }
}

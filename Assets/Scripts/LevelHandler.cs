using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] TileLevelInterpreter tileLevelInterpreter;
    public int IndexActiveWood;
    private int TotalResources;
    [SerializeField] List<GameObject> WoodResources;
    //Those MUST be in order from size 1 to size 3!!!!
    [SerializeField] GameObject[] WoodPrefabTypes;
    // Start is called before the first frame update
    void Start()
    {
        //foreach (TileLevelInterpreter.TileTypes count in tileLevelInterpreter.GetResources()) Debug.Log(count);
        PopulateResourcesFromTiling(tileLevelInterpreter.GetResources());
        TotalResources = WoodResources.Count;
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
        if (IndexActiveWood == WoodResources.Count - 1)
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

    private void PopulateResourcesFromTiling(List<TileLevelInterpreter.TileTypes> tileTypes)
    {
        for (int i = 0; i < tileTypes.Count; i++)
        {
            switch (tileTypes[i])
            {
                case TileLevelInterpreter.TileTypes.OneBlockWood:
                    WoodResources.Add(WoodPrefabTypes[0]);
                    break;
                case TileLevelInterpreter.TileTypes.TwoBlockWood:
                    WoodResources.Add(WoodPrefabTypes[1]);
                    break;
                case TileLevelInterpreter.TileTypes.ThreeBlockWood:
                    WoodResources.Add(WoodPrefabTypes[2]);
                    break;
                default:
                    throw new System.Exception("Non-wood block in Resources Array.");
            }
        }
    }
}

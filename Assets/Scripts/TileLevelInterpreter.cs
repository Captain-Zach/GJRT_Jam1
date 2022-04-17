using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileLevelInterpreter : MonoBehaviour
{
    [SerializeField] Sprite levelTileMap;
    [SerializeField] TextAsset csvColors;
    Color[] enumColors;
    TileTypes[] enumTileTypes;

    List<List<TileTypes>> tilesGrid;
    List<TileTypes> resources;

    private void Awake()
    {
        //foreach (TileLevelInterpreter.TileTypes count in GetResources()) Debug.Log(count);
    }

    public void SetSprite(Sprite levelSprite)
    {
        levelTileMap = levelSprite;
    }

    // Start is called before the first frame update
    void Start()
    {
        string message = "";
        for (int y = 0; y < GetGridTiles().Count; y++)
        {
            message += "[";
            for (int x = 0; x < GetGridTiles()[y].Count; x++)
            {
                message += GetGridTiles()[y][x].ToString() + ",";
            }
            message += "]\n";
        }
        Debug.Log(message);
    }

    public void LoadEssentials()
    {
        PopulateFromCsv(csvColors);
        PopulatefromLevelTilemap(levelTileMap);
    }

    private void PopulateFromCsv(TextAsset csv)
    {
        List<Color> returnColors = new List<Color>();
        List<TileTypes> returnTiles = new List<TileTypes>();
        string text = csv.text;
        string[] dictionary = text.Split('\n');
        foreach (string color_code in dictionary)
        {
            string[] color = color_code.Split(',');
            returnColors.Add(new Color(float.Parse(color[1]), float.Parse(color[2]), float.Parse(color[3])));
            returnTiles.Add(GetTileType(color[0]));
        }

        enumColors = returnColors.ToArray();
        enumTileTypes = returnTiles.ToArray();
    }

    void PopulatefromLevelTilemap(Sprite tilemap)
    {
        float width = tilemap.rect.width, height = tilemap.rect.height;
        List<List<TileTypes>> returnListTiles = new List<List<TileTypes>>();
        List<TileTypes> returnResources = new List<TileTypes>();

        for (int y = Mathf.RoundToInt(height); y >= 0; y--)
        {
            List<TileTypes> lineOfTypes = new List<TileTypes>();
            for (int x = 0; x < width; x++)
            {
                if (x+1 == width) 
                {
                    Color tempColorResource = tilemap.texture.GetPixel(x, y - 1);
                    TileTypes tempTileType = GetTileType(new Color(tempColorResource.r * 255, tempColorResource.g * 255, tempColorResource.b * 255));
                    if (tempTileType == 0) continue;
                    returnResources.Add(tempTileType);
                    continue;
                }
                Color tempColor = tilemap.texture.GetPixel(x, y-1);
                lineOfTypes.Add(GetTileType(new Color(tempColor.r*255, tempColor.g*255, tempColor.b*255)));
            }
            returnListTiles.Add(lineOfTypes);
        }

        tilesGrid = returnListTiles;
        resources = returnResources;
    }

    public List<List<TileTypes>> GetGridTiles()
    {
        return tilesGrid;
    }

    public List<TileTypes> GetResources()
    {
        return resources;
    }

    public TileTypes GetTileType(string tileTypeString)
    {
        if (Enum.TryParse(tileTypeString, out TileTypes tileType))
            return tileType;
        else 
            return 0;
    }

    public TileTypes GetTileType(Color tileTypeColor)
    {
        for (int i = 0; i < enumColors.Length; i++)
        {
            if (enumColors[i].Equals(tileTypeColor))
            {
                return enumTileTypes[i];
            }
        }
        return 0;
    }

    public enum TileTypes
    {
        BasicBlock = 1,
        OneBlockWood = 2,
        TwoBlockWood = 3,
        ThreeBlockWood = 4,
        WaterStream = 5,
        Fork = 6,
        BeaversHouse = 7,
        Village = 8,
        VillagesHitbox = 9,
        City = 10,
        CitysHitbox = 11
    }
}

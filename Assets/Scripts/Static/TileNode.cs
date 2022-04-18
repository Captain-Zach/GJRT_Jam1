using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNode
{
    //This is a quick test class
    public class NodeTemplate
    {
        private int WaterRange = 0;
        private bool isWaterLogged = false;
        public TileTypes Type;

        public GameObject TilePrefab;

        public enum TileTypes
        {
            None = 0,
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
            CitysHitbox = 11,
            Water = 12
        }

        public NodeTemplate(int WaterRange, bool isWaterLogged, TileTypes Type, GameObject tilePrefab)
        {
            this.WaterRange = WaterRange;
            this.isWaterLogged = isWaterLogged;
            this.Type = Type;
            TilePrefab = tilePrefab;
        }
        public NodeTemplate()
        {
            WaterRange = 0;
            isWaterLogged = false;
            Type = TileTypes.None;
        }

        public void setRange(int range){ WaterRange = range; }

        public int getRange(){ return WaterRange; }
        public void setWaterLogged(bool logged){ isWaterLogged = logged; }

        public bool getWaterLogged(){ return isWaterLogged; }
        public void setType(TileTypes type){ Type = type; }

        public TileTypes getType(){ return Type; }


    }


    public static void HelloTest()
    {
        Debug.Log("Hello world!!!!");
    }
}

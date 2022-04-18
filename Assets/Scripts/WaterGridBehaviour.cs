//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class WaterGridBehaviour
//{
//    TileNode InitialWaterTile;
//    TileNode[] Surrondings;
//    /*
//     * 0 = Left Middle (x-1, y)
//     * 1 = Water itself (x,y)
//     * 2 = Right Middle (x+1,y)
//     * 3 = Left Bottom (x-1,y-1)
//     * 4 = Middle Bottom (x,y-1)
//     * 5 = Right Bottom (x+1,y-1)
//     */


//    public WaterGridBehaviour(TileNode InitialWaterStream)
//    {
//        InitialWaterTile = InitialWaterStream;
//        Init();
//    }



//    void Init()
//    {
//        /*
//         * GetTileNode from each specific node to populate the surrondings array.
//         */
//        GridCore gridCoreComponent = InitialWaterTile.gameObject.GetComponentInParent<GridCore>();
        
//        int x = InitialWaterTile.GetXCoord(), y = InitialWaterTile.GetYCoord();
//        Surrondings = new TileNode[6];
//        Surrondings[0] = gridCoreComponent.gridMap[y][x-1].GetComponent<TileNode>();
//        Surrondings[1] = gridCoreComponent.gridMap[y][x].GetComponent<TileNode>();
//        Surrondings[2] = gridCoreComponent.gridMap[y][x+1].GetComponent<TileNode>();
//        Surrondings[3] = gridCoreComponent.gridMap[y-1][x-1].GetComponent<TileNode>();
//        Surrondings[4] = gridCoreComponent.gridMap[y-1][x].GetComponent<TileNode>();
//        Surrondings[5] = gridCoreComponent.gridMap[y-1][x+1].GetComponent<TileNode>();

//        switch (TryCreateWater(4))
//        {
//            case 0: //Sucessulf creation.
//                SucessfulCreation(Surrondings[4], 0);
//                break;
//            case 1: //Normal blocks in path.

//                switch (TryCreateWater(0))
//                {
//                    case 0:
//                        SucessfulCreation(Surrondings[0]);
//                        break;
//                    case 1:
//                        //Don't make water. Stop process. 
//                        break;
//                    case 2:
//                        ForkedCreation(Surrondings[0], 0);
//                        break;
//                    case 3:
//                        HitboxCreation(Surrondings[0], 0);
//                        break;
//                }

//                switch (TryCreateWater(2))
//                {
//                    case 0:
//                        SucessfulCreation(Surrondings[2]);
//                        break;
//                    case 1:
//                        //Don't make water. Stop process. 
//                        break;
//                    case 2:
//                        ForkedCreation(Surrondings[2], 0);
//                        break;
//                    case 3:
//                        HitboxCreation(Surrondings[2], 0);
//                        break;
//                }

//                break;
//            case 2: //Forking fork.
//                ForkedCreation(Surrondings[4], 0);
//                break;
//            case 3: //Hitbox win/lose gamestate
//                HitboxCreation(Surrondings[4], 0);
//                break;
//            case 100: //Nuclear fork (fork as center object!!)

//                switch (TryCreateWater(0))
//                {
//                    case 0:
//                        SucessfulCreation(Surrondings[0], 0);
//                        break;
//                    case 1:
//                        //Don't make water. Stop process. 
//                        break;
//                    case 2:
//                        ForkedCreation(Surrondings[0], 0);
//                        break;
//                    case 3:
//                        HitboxCreation(Surrondings[0], 0);
//                        break;
//                }

//                switch (TryCreateWater(2))
//                {
//                    case 0:
//                        SucessfulCreation(Surrondings[2], 0);
//                        break;
//                    case 1:
//                        //Don't make water. Stop process. 
//                        break;
//                    case 2:
//                        ForkedCreation(Surrondings[2], 0);
//                        break;
//                    case 3:
//                        HitboxCreation(Surrondings[2], 0);
//                        break;
//                }

//                break;
//            default: 
//                //Water can't be created inside water.
//                //Validate which water range is higher then do sucessful creation if the higher is the ladder
//                break;
//        }
//    }

//    private void SucessfulCreation(TileNode newWaterSpot, int RemainingWaterRange = 4)
//    {
//        //If remaining water range not specified, then it should reset to 4
//        //Edit node information for WaterRange = RemainingWaterRange, Type = Water, IsWaterLogged = false
//        UpdateTileInfo(newWaterSpot, TileLevelInterpreter.TileTypes.Water, false, RemainingWaterRange);
//        WaterGridBehaviour childWater = new WaterGridBehaviour(newWaterSpot);
//    }

//    private void ForkedCreation(TileNode pseudoWaterSpot, int RemainingWaterRange)
//    {
//        //Edit node information for WaterRange = RemainingWaterRange, IsWaterLogged = true

//        //This will create a fake water stream for the forked water.
//        UpdateTileInfo(pseudoWaterSpot, pseudoWaterSpot.GetTileTypes(), true, RemainingWaterRange);
//        WaterGridBehaviour childWater = new WaterGridBehaviour(pseudoWaterSpot);
//    }

//    private void HitboxCreation(TileNode hitboxWaterSpot, int RemainingWaterRange)
//    {
//        //Edit node information for WaterRange = RemainingWaterRange, IsWaterLogged = true

//        //Call in gamestate functionallity -> actually no. Lets do the reverse. A win/lose code that checks for all hitboxes?
//        UpdateTileInfo(hitboxWaterSpot, hitboxWaterSpot.GetTileTypes(), true, RemainingWaterRange);
//        WaterGridBehaviour childWater = new WaterGridBehaviour(hitboxWaterSpot);
//    }

//    private void UpdateTileInfo(TileNode tileNode, TileLevelInterpreter.TileTypes tileType, bool IsWaterLogged, int RemainingWaterRange)
//    {
//        //magic?
//    }

//    private int TryCreateWater(int whereToCreateIndex)
//    {
//        if (Surrondings[1].GetTileTypes() == TileLevelInterpreter.TileTypes.Fork) return 100; //Nuclear option of fork creation...?

//        if (Surrondings[whereToCreateIndex].GetTileTypes() == TileLevelInterpreter.TileTypes.None) return 0;
//        //Instantly return true in case of empty block.

//        if ((int)Surrondings[whereToCreateIndex].GetTileTypes() > 0 && (int)Surrondings[whereToCreateIndex].GetTileTypes() < 5)
//        {
//            //Just hit block or wood. Now TryCreate into sides.
//            return 1;
//        }
//        else if ((int)Surrondings[whereToCreateIndex].GetTileTypes() == 6)
//        {
//            //Just hit a fork. Now WaterLog it and continue to the sides.
//            return 2;
//        }
//        else if ((int)Surrondings[whereToCreateIndex].GetTileTypes() > 6 && (int)Surrondings[whereToCreateIndex].GetTileTypes() < 12)
//        {
//            //Just hit a Hitbox. Now store WaterLog information for gamestate purposes.
//            return 3;
//        }

//        //Don't do anything and just return false in case of Water Stream and normal Water block.

//        return -1;
//    }
//}

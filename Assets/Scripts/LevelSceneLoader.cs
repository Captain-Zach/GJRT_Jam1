using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSceneLoader : MonoBehaviour
{
    [SerializeField] int LevelNumber;

    public int GetLevelIndex()
    {
        return LevelNumber;
    }
}

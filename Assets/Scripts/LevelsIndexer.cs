using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsIndexer : MonoBehaviour
{
    [SerializeField] Sprite[] levels;

    private Sprite activeSprite;
    private int activeLevelIndex;

    public void StartScene(int levelNumber)
    {
        Debug.Log("[n]StartScene: " + levelNumber);
        activeLevelIndex = levelNumber - 1;
        activeSprite = levels[activeLevelIndex];
        SceneManager.LoadSceneAsync("LevelLoader");
    }

    private void OnEnable()
    {
        Debug.Log("[i]OnEnable: " + activeLevelIndex);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject[] rootObjects = scene.GetRootGameObjects();
        foreach (GameObject root in rootObjects)
        {
            if (root.TryGetComponent(out TileLevelInterpreter tileLevelInterpreter))
            {
                tileLevelInterpreter.SetSprite(activeSprite);
                tileLevelInterpreter.LoadEssentials();
            }
        }
    }
}

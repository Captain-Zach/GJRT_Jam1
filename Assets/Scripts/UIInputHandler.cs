using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIInputHandler : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    GraphicRaycaster raycaster;

    PointerEventData clickData;
    List<RaycastResult> clickRaycastResults;

    void Start()
    {
        raycaster = Canvas.GetComponent<GraphicRaycaster>();
        clickData = new PointerEventData(EventSystem.current);
        clickRaycastResults = new List<RaycastResult>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            GetUIComponent();
        }
    }

    void GetUIComponent()
    {
        clickData.position = Mouse.current.position.ReadValue();
        clickRaycastResults.Clear();

        raycaster.Raycast(clickData, clickRaycastResults);

        int levelToLoad = 0;

        foreach (RaycastResult result in clickRaycastResults)
        {
            GameObject uiElement = result.gameObject;
            if (uiElement.TryGetComponent(out LevelSceneLoader levelSceneLoader))
                levelToLoad = levelSceneLoader.GetLevelIndex();
        }

        if (levelToLoad != 0)
        {
            GetComponent<LevelsIndexer>().StartScene(levelToLoad);
        } 
    }
}

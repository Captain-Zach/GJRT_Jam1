using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WoodCreator : MonoBehaviour
{
    [SerializeField] GameObject MainCamera;
    [SerializeField] GameObject WoodPrefab;
    [SerializeField] GameObject StreamPrefab;

    public void SpawnWood(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            LevelHandler levelHandler = Object.FindObjectOfType<LevelHandler>();
            Vector2 mousePosition = MainCamera.GetComponent<Camera>().ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 roundedMousePosition = SnapVector2(mousePosition);
            Vector2 correctedSpawnPosition;
            Collider2D[] colliders;
            GameObject createdWood;

            GameObject woodPrefab = levelHandler.GetWood();

            if (levelHandler.GetTotalResources() == 0) return;

            if (woodPrefab.transform.localScale.x % 2 == 0)
            {
                correctedSpawnPosition = new Vector2(roundedMousePosition.x - 0.5f, roundedMousePosition.y);
                createdWood = Instantiate(woodPrefab, correctedSpawnPosition, transform.rotation);
                colliders = Physics2D.OverlapBoxAll(roundedMousePosition, new Vector2(0.9f * woodPrefab.transform.localScale.x, 0.9f), 0f);
            } else
            {
                correctedSpawnPosition = roundedMousePosition;
                createdWood = Instantiate(woodPrefab, correctedSpawnPosition, transform.rotation);
                colliders = Physics2D.OverlapBoxAll(roundedMousePosition, new Vector2(0.9f, 0.9f), 0f);
            }

            foreach (Collider2D collider in colliders)
            {
                //Deletes duplicate wood if it exists
                if (collider.name.Contains("Wood") && collider.gameObject != createdWood)
                {
                    Destroy(createdWood);
                    return;
                }

                if (collider.GetComponentInParent<WaterSpawnBehaviour>() != null)
                {
                    WaterSpawnBehaviour[] allWaterStreamGens = Object.FindObjectsOfType<WaterSpawnBehaviour>();
                    foreach (WaterSpawnBehaviour waterSpawn in allWaterStreamGens)
                    {
                        Transform transformCopy = waterSpawn.transform;
                        Destroy(waterSpawn.gameObject);
                        if (!waterSpawn.name.Contains("Clone"))
                        {
                            GameObject newMainWaterStream = Instantiate(StreamPrefab, transformCopy.position, transformCopy.rotation);
                            newMainWaterStream.name = "WaterSpawner";
                        }
                    }
                }
            }

            levelHandler.UseWood();
        }
    }

    public void NextWoodSize(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            LevelHandler levelHandler = Object.FindObjectOfType<LevelHandler>();
            levelHandler.GoToNextWood();
            Debug.Log(levelHandler.GetWood().name);
        }
    }

    public Vector2 SnapVector2(Vector3 vector3, float gridSize = 1.0f)
    {
        return new Vector3(
            Mathf.Round(vector3.x / gridSize) * gridSize,
            Mathf.Round(vector3.y / gridSize) * gridSize);
    }

    //public Vector2 GetFacingDirection(Vector2 mousePosition)
    //{
    //    //Positive X and Y = Top and Right
    //    //X > Y = Right
    //    //Y > X = Top

    //    //Positive X and Negative Y = Right and Down
    //    //X > Y = Right
    //    //Y > X = Down

    //    //Negative X and Y = Down and Left
    //    //X > Y = Left
    //    //Y > X = Down

    //    //Negative X and Positive Y = Left and Top
    //    //X > Y = Left
    //    //Y > X = Top


    //    if (Mathf.Abs(mousePosition.x) > Mathf.Asin(mousePosition.y))
    //    { //Left or Right
    //        if (mousePosition.x > 0)
    //        {//Right
    //            return Vector2.right;
    //        }
    //        else
    //        {//Left
    //            return Vector2.left;
    //        }
    //    }
    //    else
    //    { //Top or Down
    //        if (mousePosition.y > 0)
    //        {//Top
    //            return Vector2.up;
    //        }
    //        else
    //        {//Down
    //            return Vector2.down;
    //        }
    //    }
    //}
}

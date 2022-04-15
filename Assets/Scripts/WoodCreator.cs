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
            Vector2 mousePosition = MainCamera.GetComponent<Camera>().ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3 diffMousePlayer = new Vector3(mousePosition.x, mousePosition.y, 0) - gameObject.transform.position;
            Vector2 spawningDirection = GetFacingDirection(diffMousePlayer);
            Vector3 spawningPosition = new Vector3(spawningDirection.x + transform.position.x, spawningDirection.y + transform.position.y, 0);
            Instantiate(WoodPrefab, spawningPosition, transform.rotation);

            Collider2D[] colliders = Physics2D.OverlapBoxAll(spawningPosition, new Vector2(0.9f, 0.9f), 0f);

            foreach (Collider2D collider in colliders)
            {
                if (collider.GetComponentInParent<WaterSpawnBehaviour>() != null)
                {
                    GameObject waterStreamGenerator = collider.GetComponentInParent<WaterSpawnBehaviour>().gameObject;
                    Instantiate(StreamPrefab, waterStreamGenerator.transform.position, waterStreamGenerator.transform.rotation);
                    Destroy(waterStreamGenerator);
                }
            }
        }
    }

    public void SelectWoodSize(InputAction.CallbackContext context)
    {

    }


    public Vector2 GetFacingDirection(Vector2 mousePosition)
    {
        //Positive X and Y = Top and Right
        //X > Y = Right
        //Y > X = Top

        //Positive X and Negative Y = Right and Down
        //X > Y = Right
        //Y > X = Down

        //Negative X and Y = Down and Left
        //X > Y = Left
        //Y > X = Down

        //Negative X and Positive Y = Left and Top
        //X > Y = Left
        //Y > X = Top


        if (Mathf.Abs(mousePosition.x) > Mathf.Asin(mousePosition.y))
        { //Left or Right
            if (mousePosition.x > 0)
            {//Right
                return Vector2.right;
            }
            else
            {//Left
                return Vector2.left;
            }
        }
        else
        { //Top or Down
            if (mousePosition.y > 0)
            {//Top
                return Vector2.up;
            }
            else
            {//Down
                return Vector2.down;
            }
        }
    }
}

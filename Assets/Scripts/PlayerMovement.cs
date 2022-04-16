using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public void Move(float x, float y)
    {
        transform.position += new Vector3(x, y, 0);
    }

    public void OnUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Move(0, 1);
        }
    }

    public void OnDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Move(0, -1);
        }
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Move(-1, 0);
        }
    }

    public void OnRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Move(1, 0);
        }
    }


}

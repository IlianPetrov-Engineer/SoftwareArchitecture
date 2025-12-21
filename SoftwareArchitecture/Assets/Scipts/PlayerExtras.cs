using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerExtras : MonoBehaviour
{
    public static Action onInteract;

    public void Interact(InputAction.CallbackContext context)
    {
        onInteract?.Invoke();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

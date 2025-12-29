using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerExtras : MonoBehaviour
{
    public PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponentInChildren<PlayerInput>();
    }

    //private ProjectileControler projectileControler;

    //private void Start()
    //{
    //    projectileControler = GetComponent<ProjectileControler>();
    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    projectileControler.Attack();
        //}
    }

    void OnTest(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Test");
        }
    }
}

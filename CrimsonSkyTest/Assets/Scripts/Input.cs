using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Input : MonoBehaviour
{
    public static event Action<Vector3> OnClickToMove;

    private PlayerInput input;

    public Vector2 direction;

    public LayerMask groundMask;

    private void Awake()
    {
        input = new PlayerInput();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += ctx => direction = ctx.ReadValue<Vector2>(); // Pass value of x/y axis to public variable
        input.Player.Click.performed += Clicked;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= ctx => direction = ctx.ReadValue<Vector2>();
        input.Player.Click.performed -= Clicked;
    }

    private void Clicked(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.cyan, 0.7f);

        // Raycast mouse position & check valid hit against ground
        if (Physics.Raycast(ray, out hit, 1000, groundMask))
        {
            Debug.Log(hit.point);
            OnClickToMove?.Invoke(hit.point);
        }
    }

}

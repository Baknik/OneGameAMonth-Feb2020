using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputCollector : MonoBehaviour
{
    [Header("Runtime")]
    public bool SelectInput;
    public Vector3 MouseScreenPosition;

    private PlayerInput playerInput;

    void Awake()
    {
        this.playerInput = this.GetComponent<PlayerInput>();
    }

    void Start()
    {
        this.SelectInput = false;
    }

    void Update()
    {
        this.MouseScreenPosition = Input.mousePosition;
    }

    public void OnSelect(InputValue value)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            this.SelectInput = true;
        }
    }
}

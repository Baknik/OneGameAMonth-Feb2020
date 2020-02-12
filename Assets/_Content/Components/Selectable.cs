using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Selectable : MonoBehaviour
{
    [Header("Settings")]
    public int SelectionPriority = 0;

    [Header("Runtime")]
    public bool Selected;
    public bool Hovering;

    [HideInInspector]
    public Collider Collider;
    
    void Awake()
    {
        this.Collider = this.GetComponent<Collider>();
    }
}
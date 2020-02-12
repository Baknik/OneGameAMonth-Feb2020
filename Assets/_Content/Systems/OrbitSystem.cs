using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class OrbitSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Orbit orbit, MeshRenderer renderer, Selectable selectable) =>
        {
            Color orbitColor = orbit.BaseColor.Value;
            if (selectable.Selected)
            {
                orbitColor = orbit.SelectedColor.Value;
            }
            else if (selectable.Hovering)
            {
                orbitColor = orbit.HoverColor.Value;
            }

            renderer.material.SetColor("_Color", orbitColor);
        });
    }
}

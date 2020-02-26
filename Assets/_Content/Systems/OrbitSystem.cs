using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class OrbitSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        InitEntityQueryCache(15);

        // Selection color
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

        // Orbital speed based on radius
        Entities.ForEach((Orbit orbit) =>
        {
            Entities.ForEach((Satellite satellite, WorldRotator worldRotator) =>
            {
                if (orbit.Satellite == satellite)
                {
                    worldRotator.Speed = Mathf.Atan(satellite.OrbitingSpeed / orbit.Radius) * Mathf.Rad2Deg;
                }
            });
        });
    }
}

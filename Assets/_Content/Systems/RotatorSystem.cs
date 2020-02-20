using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class RotatorSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        Entities.ForEach((LocalRotator localRotator) =>
        {
            Vector3 axis = new Vector3(localRotator.Axis.X ? 1f : 0f, localRotator.Axis.Y ? 1f : 0f, localRotator.Axis.Z ? 1f : 0f);
            localRotator.transform.Rotate(axis, localRotator.Speed * deltaTime, Space.Self);
        });

        Entities.ForEach((WorldRotator worldRotator) =>
        {
            Vector3 axis = new Vector3(worldRotator.Axis.X ? 1f : 0f, worldRotator.Axis.Y ? 1f : 0f, worldRotator.Axis.Z ? 1f : 0f);
            worldRotator.transform.RotateAround(worldRotator.Origin, axis, worldRotator.Speed * deltaTime);
        });
    }
}

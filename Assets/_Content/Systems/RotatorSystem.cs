using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class RotatorSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        Entities.ForEach((Rotator rotator) =>
        {
            Vector3 axis = new Vector3(rotator.Axis.X ? 1f : 0f, rotator.Axis.Y ? 1f : 0f, rotator.Axis.Z ? 1f : 0f);
            rotator.transform.Rotate(axis, rotator.Speed * deltaTime);
        });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class DestroySystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        InitEntityQueryCache(15);

        Entities.ForEach((Entity entity, Destroy destroy) =>
        {
            destroy.TimeAlive += Time.DeltaTime;
            if (destroy.TimeAlive >= destroy.Delay)
            {
                PostUpdateCommands.DestroyEntity(entity);
                GameObject.Destroy(destroy.gameObject);
            }
        });
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class RadialSpawnerSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        InitEntityQueryCache(15);

        Entities.ForEach((RadialSpawner spawner) =>
        {
            if ((UnityEngine.Time.time - spawner.LastSpawnTime) >= spawner.NextWaitPeriod)
            {
                // Create object
                Vector2 spawnDirection = Random.insideUnitCircle.normalized;
                GameObject spawnedObject =
                    PrefabFactory.Instance.InstantiatePrefab(spawner.PrefabName, spawnDirection * spawner.SpawnRadius, Quaternion.identity, null);

                // Random scale
                float objectScale = Random.Range(spawner.ScaleRangeTiers[spawner.Tier].Min, spawner.ScaleRangeTiers[spawner.Tier].Max);
                spawnedObject.transform.localScale = new Vector3(objectScale, objectScale, objectScale);
                
                // Reset timer
                spawner.NextWaitPeriod = Random.Range(spawner.FrequencyRangeTiers[spawner.Tier].Min, spawner.FrequencyRangeTiers[spawner.Tier].Max);
                spawner.LastSpawnTime = UnityEngine.Time.time;
            }
        });
    }
}

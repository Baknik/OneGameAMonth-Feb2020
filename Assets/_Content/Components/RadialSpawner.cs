using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialSpawner : MonoBehaviour
{
    [Header("Settings")]
    public string PrefabName;
    public float SpawnRadius;
    public FloatRange ScaleRange;
    public FloatRange FrequencyRange;

    [HideInInspector]
    public float LastSpawnTime;
    [HideInInspector]
    public float NextWaitPeriod;

    void Start()
    {
        this.LastSpawnTime = Time.time;
        this.NextWaitPeriod = 0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialSpawner : MonoBehaviour
{
    [Header("Settings")]
    public string PrefabName;
    public float SpawnRadius;
    public FloatRange[] ScaleRangeTiers;
    public FloatRange[] FrequencyRangeTiers;

    [HideInInspector]
    public float LastSpawnTime;
    [HideInInspector]
    public float NextWaitPeriod;
    [HideInInspector]
    public int Tier;

    void Start()
    {
        this.LastSpawnTime = Time.time;
        this.NextWaitPeriod = 0f;
        this.Tier = 0;
    }

    public void IncreaseTier()
    {
        if (this.Tier < (ScaleRangeTiers.Length - 1) && this.Tier < (FrequencyRangeTiers.Length - 1))
        {
            this.Tier++;
        }
    }
}

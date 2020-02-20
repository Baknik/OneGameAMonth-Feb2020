using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("Settings")]
    public LineRenderer LineRenderer;
    public float Range;
    public float TickFrequency;
    public float AimSpeed;
    public Damage BaseTickDamage;
    public float AddedDamagePerTick;

    [HideInInspector]
    public double LastTickTime;
    [HideInInspector]
    public int NumConsecutiveTicks;

    void Start()
    {
        this.LastTickTime = 0d;
        this.NumConsecutiveTicks = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    [Header("Settings")]
    public string Name;
    public FloatReference SlowOrbitingSpeed;
    public FloatReference MediumOrbitingSpeed;
    public FloatReference FastOrbitingSpeed;
    public SatelliteStats BaseStats;
    public SatelliteStats StatIncreasePerLevel;
    public FloatReference UpgradeCostPerLevel;
    public FloatReference MaxLevel;
    [TextArea]
    public string Description;

    [HideInInspector]
    public float Value;
    [HideInInspector]
    public float Level;
    [HideInInspector]
    public float OrbitingSpeed;
    [HideInInspector]
    public SatelliteStats Stats;

    void Start()
    {
        this.Level = 1f;
        this.OrbitingSpeed = this.MediumOrbitingSpeed.Value;
        this.Stats = this.BaseStats;
    }
}

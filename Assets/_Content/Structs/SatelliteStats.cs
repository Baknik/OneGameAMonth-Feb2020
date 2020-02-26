using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "SatelliteStats.asset", menuName = "Variables/SatelliteStats", order = 1)]
public class SatelliteStats : ScriptableObject
{
    public float PrimaryMagnitude;
    public float SecondaryMagnitude;
    public float Reach;
    public List<DamageResistance> DamageResistances;
}

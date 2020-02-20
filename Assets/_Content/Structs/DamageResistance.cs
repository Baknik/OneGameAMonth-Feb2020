using System;
using UnityEngine;

[Serializable]
public struct DamageResistance
{
    [Range(0f,100f)]
    public float Percentage;
    public DamageType Type;
}

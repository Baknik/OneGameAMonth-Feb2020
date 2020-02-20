using System;
using UnityEngine;

[Serializable]
public struct EffectResistance
{
    [Range(0f,100f)]
    public float Percentage;
    public EffectType Type;
}

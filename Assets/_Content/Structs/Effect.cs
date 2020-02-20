using System;
using UnityEngine;

[Serializable]
public struct Effect
{
    public EffectType Type;
    public float Magnitude;
    public float Duration;
    public bool Stackable;
}

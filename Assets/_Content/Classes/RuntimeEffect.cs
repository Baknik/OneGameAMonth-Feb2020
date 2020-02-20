using System;
using UnityEngine;

public class RuntimeEffect
{
    public EffectType Type;
    public float Magnitude;
    public float Duration;

    public RuntimeEffect(Effect effect)
    {
        this.Type = effect.Type;
        this.Magnitude = effect.Magnitude;
        this.Duration = effect.Duration;
    }
}

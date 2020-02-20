using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class EffectsSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Effector effector) =>
        {
            // Duration
            foreach (EffectType effectType in effector.Effects.Keys)
            {
                foreach (RuntimeEffect effect in effector.Effects[effectType])
                {
                    effect.Duration -= Time.DeltaTime;
                }
                effector.Effects[effectType].RemoveAll(effect => effect.Duration <= 0f);
            }
        });
    }
}

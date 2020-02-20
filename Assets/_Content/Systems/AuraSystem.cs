using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class AuraSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Aura aura) =>
        {
            Entities.ForEach((Effector effector) =>
            {
                if (Vector3.Distance(aura.transform.position, effector.transform.position) <= aura.Range)
                {
                    foreach (Effect effect in aura.EffectsApplied)
                    {
                        if (!effector.Effects.ContainsKey(effect.Type))
                            effector.Effects.Add(effect.Type, new List<RuntimeEffect>());

                        // Resists
                        float resistanceMultiplier = 1f;
                        foreach (EffectResistance resist in effector.Resistances)
                        {
                            if (resist.Type == effect.Type)
                            {
                                resistanceMultiplier = Mathf.Clamp(1f - (resist.Percentage / 100f), 0f, 1f);
                                break;
                            }
                        }
                        if (resistanceMultiplier > 0f) // immune check
                        {
                            // Apply the effect
                            RuntimeEffect appliedEffect = new RuntimeEffect(effect);
                            if (!effect.Stackable)
                            {
                                effector.Effects[appliedEffect.Type].Clear();
                            }
                            effector.Effects[appliedEffect.Type].Add(appliedEffect);
                        }
                    }
                }
            });

            // Boundary
            if (aura.Boundary != null)
            {
                aura.Boundary.localScale = new Vector3(2f * aura.Range, 2f * aura.Range, aura.Boundary.localScale.z);
            }
        });
    }
}

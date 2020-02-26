using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class SatelliteSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        InitEntityQueryCache(15);

        // Gun
        Entities.ForEach((Satellite satellite, Gun gun, Health health) =>
        {
            SatelliteStats stats = this.CalculateStats(satellite);
            gun.ShootFrequency = stats.PrimaryMagnitude;
            gun.AimSpeed = stats.SecondaryMagnitude;
            gun.ShootRange = stats.Reach;
            gun.DetectRange = stats.Reach;
            health.DamageResistances = stats.DamageResistances;
        });

        // Ram
        Entities.WithNone<Gun, Laser, Aura>().ForEach((Satellite satellite, Health health) =>
        {
            SatelliteStats stats = this.CalculateStats(satellite);
            health.ImpactDamage = new Damage()
            {
                Amount = stats.PrimaryMagnitude,
                Type = DamageType.PHYSICAL
            };
            satellite.transform.localScale = new Vector3(stats.Reach, stats.Reach, stats.Reach);
            health.DamageResistances = stats.DamageResistances;
        });

        // Laser
        Entities.ForEach((Satellite satellite, Laser laser, Health health) =>
        {
            SatelliteStats stats = this.CalculateStats(satellite);
            laser.BaseTickDamage = new Damage()
            {
                Amount = stats.PrimaryMagnitude,
                Type = DamageType.HEAT
            };
            laser.AddedDamagePerTick = stats.SecondaryMagnitude;
            laser.Range = stats.Reach;
            health.DamageResistances = stats.DamageResistances;
        });

        // Chill
        Entities.ForEach((Satellite satellite, Aura aura, Health health) =>
        {
            SatelliteStats stats = this.CalculateStats(satellite);
            List<Effect> newEffects = new List<Effect>();
            foreach (Effect effect in aura.EffectsApplied)
            {
                if (effect.Type == EffectType.CHILL)
                {
                    newEffects.Add(new Effect()
                    {
                        Duration = stats.SecondaryMagnitude,
                        Magnitude = stats.PrimaryMagnitude,
                        Stackable = effect.Stackable,
                        Type = effect.Type
                    });
                    aura.EffectsApplied = newEffects;
                    break;
                }
            }
            aura.Range = stats.Reach;
            health.DamageResistances = stats.DamageResistances;
        });
    }

    private SatelliteStats CalculateStats(Satellite satellite)
    {
        List<DamageResistance> resists = new List<DamageResistance>();
        foreach (DamageResistance baseResist in satellite.BaseStats.DamageResistances)
        {
            foreach (DamageResistance resistPerLevel in satellite.StatIncreasePerLevel.DamageResistances)
            {
                if (resistPerLevel.Type == baseResist.Type)
                {
                    float resistValue = Mathf.Clamp(baseResist.Percentage + (resistPerLevel.Percentage * (satellite.Level - 1f)), 0f, 100f);
                    resists.Add(new DamageResistance()
                    {
                        Percentage = resistValue,
                        Type = baseResist.Type
                    });
                    break;
                }
            }
        }

        return new SatelliteStats()
        {
            PrimaryMagnitude = satellite.BaseStats.PrimaryMagnitude + (satellite.StatIncreasePerLevel.PrimaryMagnitude * (satellite.Level - 1f)),
            SecondaryMagnitude = satellite.BaseStats.SecondaryMagnitude + (satellite.StatIncreasePerLevel.SecondaryMagnitude * (satellite.Level - 1f)),
            Reach = satellite.BaseStats.Reach + (satellite.StatIncreasePerLevel.Reach * (satellite.Level - 1f)),
            DamageResistances = resists
        };
    }
}

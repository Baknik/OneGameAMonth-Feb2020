using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BodySystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        InitEntityQueryCache(15);

        Planet planet = null;
        Entities.ForEach((Planet p) =>
        {
            planet = p;
        });

        Entities.ForEach((Body body, Rigidbody rigidbody) =>
        {
            // Chill
            float chillMultiplier = 1f;
            Entities.ForEach((Body effectorBody, Effector effector) =>
            {
                if (effectorBody == body && effector.Effects.ContainsKey(EffectType.CHILL) && effector.Effects[EffectType.CHILL].Count > 0)
                {
                    chillMultiplier = 1f - Mathf.Clamp(effector.Effects[EffectType.CHILL][0].Magnitude, 0f, 1f);
                }
            });
            body.MeshRenderer.material.SetColor("_Tint", chillMultiplier == 1f ? Color.white : Color.blue);

            // Set velocity
            if (planet != null)
            {
                Vector2 direction = (planet.transform.position - body.transform.position).normalized;
                rigidbody.velocity =
                    direction * Mathf.Lerp(body.SpeedRange.Max, body.SpeedRange.Min, Mathf.Clamp(body.transform.localScale.x, 0f, 1f)) * chillMultiplier;
            }
        });

        Entities.ForEach((Body body, Health health) =>
        {
            // Set max health
            bool raiseCurrentHealth = (health.CurrentHealth == health.MaxHealth);
            health.MaxHealth = Mathf.Floor(Mathf.Lerp(body.MaxHealthRange.Min, body.MaxHealthRange.Max, Mathf.Clamp(body.transform.localScale.x, 0f, 1f)));
            if (raiseCurrentHealth)
            {
                health.CurrentHealth = health.MaxHealth;
            }
        });
    }
}

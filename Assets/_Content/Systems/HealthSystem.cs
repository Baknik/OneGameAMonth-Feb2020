using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using UnityEngine.UI;

public class HealthSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, Health health) =>
        {
            // Health bar setup
            if (health.HealthBarSlider == null)
            {
                GameObject healthBarObject =
                    PrefabFactory.Instance.InstantiatePrefab("HealthBar", new Vector3(0f, 0f, -2.5f), Quaternion.identity, null);
                healthBarObject.transform.SetParent(health.transform, false);
                Slider slider = healthBarObject.GetComponentInChildren<Slider>();
                if (slider != null)
                {
                    health.HealthBarSlider = slider;
                }
            }

            // Intra-health collisions
            while (health.Collisions.Count > 0)
            {
                Collision collision = health.Collisions.Dequeue();
                Health otherHealth = collision.gameObject.GetComponent<Health>();
                if (otherHealth != null)
                {
                    health.Damage.Enqueue(new Damage()
                    {
                        Amount = otherHealth.MaxHealth,
                        Type = DamageType.PHYSICAL
                    });
                    otherHealth.Damage.Enqueue(new Damage()
                    {
                        Amount = health.MaxHealth,
                        Type = DamageType.PHYSICAL
                    });
                }
            }

            // Process damage
            while (health.Damage.Count > 0)
            {
                Damage damage = health.Damage.Dequeue();
                health.CurrentHealth -= damage.Amount;
            }

            // Death
            if (health.CurrentHealth <= 0f)
            {
                PostUpdateCommands.DestroyEntity(entity);
                GameObject.Destroy(health.gameObject);
            }

            // Clamp
            health.CurrentHealth = Mathf.Clamp(health.CurrentHealth, 0f, health.MaxHealth);

            // Health bar update
            if (!health.AlwaysShowHealth && health.CurrentHealth == health.MaxHealth)
            {
                health.HealthBarSlider.gameObject.SetActive(false);
            }
            else
            {
                health.HealthBarSlider.gameObject.SetActive(true);
            }
            health.HealthBarSlider.minValue = 0f;
            health.HealthBarSlider.maxValue = health.MaxHealth;
            health.HealthBarSlider.value = health.CurrentHealth;
        });
    }
}

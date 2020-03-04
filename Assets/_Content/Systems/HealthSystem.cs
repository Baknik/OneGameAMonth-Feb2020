using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using UnityEngine.UI;

public class HealthSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        InitEntityQueryCache(15);

        Money money = null;
        Entities.ForEach((Money m) =>
        {
            money = m;
        });

        Planet planet = null;
        Entities.ForEach((Planet p) =>
        {
            planet = p;
        });

        Entities.ForEach((Entity entity, Health health) =>
        {
            // Health bar setup
            if (health.HealthBarSlider == null)
            {
                GameObject healthBarObject =
                    PrefabFactory.Instance.InstantiatePrefab("HealthBar", new Vector3(1000f, 0f, 0f), Quaternion.identity, null);
                ObjectAttachedUI objectAttachedUI = healthBarObject.GetComponent<ObjectAttachedUI>();
                if (objectAttachedUI != null)
                {
                    objectAttachedUI.Object = health.gameObject.transform;
                }
                Slider slider = healthBarObject.GetComponentInChildren<Slider>();
                if (slider != null)
                {
                    health.HealthBarSlider = slider;
                }
            }

            // Intra-health collisions
            while (health.Collisions.Count > 0)
            {
                CollisionData collision = health.Collisions.Dequeue();

                // Impact
                PrefabFactory.Instance.InstantiatePrefab("ImpactVFX", collision.Contacts[0].point, Quaternion.LookRotation(collision.Contacts[0].normal), null);

                // Damage
                Health otherHealth = collision.Other.GetComponent<Health>();
                if (otherHealth != null)
                {
                    otherHealth.Damage.Enqueue(health.ImpactDamage);
                }
            }

            // Process damage
            while (health.Damage.Count > 0)
            {
                Damage damage = health.Damage.Dequeue();
                float resistance = 0f;
                foreach (DamageResistance dr in health.DamageResistances)
                {
                    if (dr.Type == damage.Type)
                    {
                        resistance = dr.Percentage;
                        break;
                    }
                }
                health.CurrentHealth -= damage.Amount * (1f - resistance / 100f);
            }

            // Death
            if (health.CurrentHealth <= 0f)
            {
                // Death money
                if (!health.RewardGiven)
                {
                    float newMoneyValue = money.CurrentMoney + (health.MoneyRewardPerHealth.Value * health.MaxHealth);
                    money.CurrentMoney = (newMoneyValue <= money.MaxMoney.Value) ? newMoneyValue : money.MaxMoney.Value;
                    health.RewardGiven = true;
                }

                PostUpdateCommands.DestroyEntity(entity);
                GameObject.Destroy(health.gameObject);

                // Death explosion
                PrefabFactory.Instance.InstantiatePrefab(health.DeathExplosionPrefab.Value, health.transform.position, Quaternion.identity, null);
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

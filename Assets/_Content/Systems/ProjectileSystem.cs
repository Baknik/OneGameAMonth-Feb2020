using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class ProjectileSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        InitEntityQueryCache(15);

        Entities.ForEach((Entity entity, Projectile projectile) =>
        {
            // Health impact
            while (projectile.Collisions.Count > 0)
            {
                CollisionData collision = projectile.Collisions.Dequeue();

                // Impact
                PrefabFactory.Instance.InstantiatePrefab("ImpactVFX", collision.Contacts[0].point, Quaternion.LookRotation(collision.Contacts[0].normal), null);

                // Damage
                Health otherHealth = collision.Other.GetComponent<Health>();
                if (otherHealth != null)
                {
                    if (projectile.DealsDamageOnImpact)
                    {
                        otherHealth.Damage.Enqueue(projectile.ImpactDamage);
                    }
                }

                PostUpdateCommands.DestroyEntity(entity);
                GameObject.Destroy(projectile.gameObject);
            }
        });
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class GunSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Gun gun) =>
        {
            Enemy closestEnemy = null;
            float closestEnemyDistance = 0f;
            Entities.ForEach((Enemy enemy) =>
            {
                float distanceToEnemy = Vector3.Distance(gun.transform.position, enemy.transform.position);
                if (closestEnemy == null)
                {
                    closestEnemy = enemy;
                    closestEnemyDistance = distanceToEnemy;
                }
                else if (distanceToEnemy < closestEnemyDistance)
                {
                    closestEnemy = enemy;
                    closestEnemyDistance = distanceToEnemy;
                }
            });
            
            // Rotate towards enemy
            if (closestEnemy != null && closestEnemyDistance <= gun.DetectRange)
            {
                var q = Quaternion.LookRotation(closestEnemy.transform.position - gun.transform.position);
                gun.transform.rotation = Quaternion.RotateTowards(gun.transform.rotation, q, gun.AimSpeed * Time.DeltaTime);
            }

            // Shoot enemy
            if (closestEnemy != null && closestEnemyDistance <= gun.ShootRange)
            {
                float sightAngle = Vector3.Angle(closestEnemy.transform.position - gun.transform.position, gun.transform.forward);
                if (sightAngle <= 5f && ((Time.ElapsedTime - gun.LastShotTakenTime) >= gun.ShootFrequency))
                {
                    GameObject bulletObject = PrefabFactory.Instance.InstantiatePrefab(gun.BulletPrefabName, gun.transform.position, Quaternion.identity, null);
                    Rigidbody bulletRigidbody = bulletObject.GetComponent<Rigidbody>();
                    if (bulletRigidbody != null)
                    {
                        bulletRigidbody.velocity = gun.transform.forward * gun.BulletSpeed;
                    }
                    gun.LastShotTakenTime = Time.ElapsedTime;
                }
            }
        });
    }
}

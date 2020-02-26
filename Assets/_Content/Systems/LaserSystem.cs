using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class LaserSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        InitEntityQueryCache(15);

        Entities.ForEach((Laser laser) =>
        {
            Enemy closestEnemy = null;
            float closestEnemyDistance = 0f;
            Entities.ForEach((Enemy enemy) =>
            {
                float distanceToEnemy = Vector3.Distance(laser.transform.position, enemy.transform.position);
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
            if (closestEnemy != null && closestEnemyDistance <= laser.Range)
            {
                var q = Quaternion.LookRotation(closestEnemy.transform.position - laser.transform.position);
                laser.transform.rotation = Quaternion.RotateTowards(laser.transform.rotation, q, laser.AimSpeed * Time.DeltaTime);
            }

            // Laser
            Vector3 currentEndPosition = laser.LineRenderer.GetPosition(1);
            if (closestEnemy != null && closestEnemyDistance <= laser.Range)
            {
                float sightAngle = Vector3.Angle(closestEnemy.transform.position - laser.transform.position, laser.transform.forward);
                if (sightAngle <= 5f)
                {
                    // Laser length
                    laser.LineRenderer.SetPosition(1, new Vector3(currentEndPosition.x, currentEndPosition.y, (2.5f * closestEnemyDistance)));

                    // Tick
                    if (((Time.ElapsedTime - laser.LastTickTime) >= laser.TickFrequency))
                    {
                        Health enemyHealth = null;
                        Entities.ForEach((Enemy enemy, Health health) =>
                        {
                            if (enemy == closestEnemy)
                            {
                                enemyHealth = health;
                            }
                        });
                        if (enemyHealth != null)
                        {
                            Damage tickDamage = new Damage()
                            {
                                Amount = laser.BaseTickDamage.Amount + (laser.AddedDamagePerTick * (float)laser.NumConsecutiveTicks),
                                Type = laser.BaseTickDamage.Type
                            };
                            enemyHealth.Damage.Enqueue(tickDamage);
                            laser.NumConsecutiveTicks++;
                            laser.LastTickTime = Time.ElapsedTime;
                        }
                    }
                }
                else
                {
                    laser.LineRenderer.SetPosition(1, new Vector3(currentEndPosition.x, currentEndPosition.y, 0f));
                    laser.NumConsecutiveTicks = 0;
                }
            }
            else
            {
                laser.LineRenderer.SetPosition(1, new Vector3(currentEndPosition.x, currentEndPosition.y, 0f));
                laser.NumConsecutiveTicks = 0;
            }
        });
    }
}

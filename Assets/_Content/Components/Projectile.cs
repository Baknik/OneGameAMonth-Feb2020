using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Settings")]
    public bool DealsDamageOnImpact;
    public Damage ImpactDamage;

    [HideInInspector]
    public Queue<CollisionData> Collisions = new Queue<CollisionData>();

    void OnCollisionEnter(Collision collision)
    {
        this.Collisions.Enqueue(new CollisionData()
        {
            Other = collision.gameObject,
            Contacts = collision.contacts
        });
    }
}

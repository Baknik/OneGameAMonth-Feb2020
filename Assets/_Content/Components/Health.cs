using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class Health : MonoBehaviour
{
    [Header("Settings")]
    public float MaxHealth;
    public bool AlwaysShowHealth;
    public Damage ImpactDamage;
    public FloatReference MoneyRewardPerHealth;
    public List<DamageResistance> DamageResistances;

    [Header("Runtime")]
    public float CurrentHealth;

    [HideInInspector]
    public Queue<Damage> Damage = new Queue<Damage>();
    [HideInInspector]
    public Queue<CollisionData> Collisions = new Queue<CollisionData>();
    [HideInInspector]
    public Slider HealthBarSlider;
    [HideInInspector]
    public bool RewardGiven;

    void Start()
    {
        this.CurrentHealth = this.MaxHealth;
        this.RewardGiven = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        this.Collisions.Enqueue(new CollisionData()
        {
            Other = collision.gameObject,
            Contacts = collision.contacts
        });
    }

    void OnTriggerEnter(Collider other)
    {
        this.Collisions.Enqueue(new CollisionData()
        {
            Other = other.gameObject
        });
    }
}

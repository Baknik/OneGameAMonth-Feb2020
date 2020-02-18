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

    [Header("Runtime")]
    public float CurrentHealth;

    [HideInInspector]
    public Queue<Damage> Damage = new Queue<Damage>();
    [HideInInspector]
    public Queue<Collision> Collisions = new Queue<Collision>();
    [HideInInspector]
    public Slider HealthBarSlider;

    void Start()
    {
        this.CurrentHealth = this.MaxHealth;
    }

    void OnCollisionEnter(Collision collision)
    {
        this.Collisions.Enqueue(collision);
    }
}

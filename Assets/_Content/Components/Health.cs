using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Settings")]
    public float StartHealth;

    [Header("Runtime")]
    public float CurrentHealth;
    public Queue<Damage> Damage;

    void Start()
    {
        this.CurrentHealth = this.StartHealth;
    }
}

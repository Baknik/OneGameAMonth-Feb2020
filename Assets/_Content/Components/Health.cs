using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float StartHealth;

    [HideInInspector]
    public float CurrentHealth;
    public Queue<Damage> Damage;

    void Start()
    {
        this.CurrentHealth = this.StartHealth;
    }
}

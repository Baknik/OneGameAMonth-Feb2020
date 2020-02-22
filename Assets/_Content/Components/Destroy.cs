using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [Header("Settings")]
    public float Delay;

    [HideInInspector]
    public double TimeAlive;

    void Start()
    {
        this.TimeAlive = 0d;
    }
}


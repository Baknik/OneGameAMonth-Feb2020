using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [Header("Settings")]
    public FloatReference StartMoney;
    public FloatReference MaxMoney;

    [HideInInspector]
    public float CurrentMoney;

    void Start()
    {
        this.CurrentMoney = StartMoney.Value;
    }
}

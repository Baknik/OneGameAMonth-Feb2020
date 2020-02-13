using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [Header("References")]
    public Text HealthText;

    [HideInInspector]
    public Slider HealthSlider;

    void Awake()
    {
        this.HealthSlider = this.GetComponentInChildren<Slider>();
    }
}

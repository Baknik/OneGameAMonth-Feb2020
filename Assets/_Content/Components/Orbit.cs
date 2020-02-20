using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [Header("Settings")]
    public float Radius;
    public ColorReference BaseColor;
    public ColorReference HoverColor;
    public ColorReference SelectedColor;
    
    public Satellite Satellite = null;
}

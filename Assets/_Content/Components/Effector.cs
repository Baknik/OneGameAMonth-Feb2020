using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effector : MonoBehaviour
{
    [Header("Settings")]
    public List<EffectResistance> Resistances;

    public Dictionary<EffectType, List<RuntimeEffect>> Effects = new Dictionary<EffectType, List<RuntimeEffect>>();
}

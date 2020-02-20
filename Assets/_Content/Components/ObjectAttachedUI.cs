using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAttachedUI : MonoBehaviour
{
    [Header("Settings")]
    public Vector3 Offset;

    [HideInInspector]
    public Transform Object;
}

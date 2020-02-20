using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Settings")]
    public Vector3 BulletSpawnOffset;
    public string BulletPrefabName;
    public float DetectRange;
    public float ShootRange;
    public float ShootFrequency;
    public float AimSpeed;
    public float BulletSpeed;

    [HideInInspector]
    public double LastShotTakenTime;

    void Start()
    {
        this.LastShotTakenTime = 0d;
    }
}

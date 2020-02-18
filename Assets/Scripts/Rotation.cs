using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [Header("Settings")]
    public AxisConstraints Axis;
    public float Speed;

    void Update()
    {
        float deltaTime = Time.deltaTime;
        Vector3 axis = new Vector3(this.Axis.X ? 1f : 0f, this.Axis.Y ? 1f : 0f, this.Axis.Z ? 1f : 0f);
        this.transform.Rotate(axis, this.Speed * deltaTime);
    }
}

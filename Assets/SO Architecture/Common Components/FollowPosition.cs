using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    [Header("Settings")]
    public BoolReference LockedOn;
    public FloatReference FollowSpeed;
    public Vector3Reference Offset;
    public OptionalFloatSetting MaxFollowDistance;
    public UpdateType UpdateType;
    public AxisConstraints FixedAxis;

    [Header("Shared")]
    public Vector3Reference TargetPosition;

    void Update()
    {
        if (this.UpdateType == UpdateType.UPDATE)
        {
            this.Follow(Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        if (this.UpdateType == UpdateType.FIXED_UPDATE)
        {
            this.Follow(Time.fixedDeltaTime);
        }
    }

    void LateUpdate()
    {
        if (this.UpdateType == UpdateType.LATE_UPDATE)
        {
            this.Follow(Time.deltaTime);
        }
    }

    private void Follow(float deltaTime)
    {
        // Follow Distance
        if (this.MaxFollowDistance.Enabled &&
            Vector3.Distance(this.transform.position, this.TargetPosition.Value) > this.MaxFollowDistance.Value.Value)
        {
            return;
        }

        // Offset
        Vector3 targetPosition = this.TargetPosition.Value + this.Offset.Value;

        // Constraints
        if (this.FixedAxis.X)
        {
            targetPosition.x = this.transform.position.x;
        }
        if (this.FixedAxis.Y)
        {
            targetPosition.y = this.transform.position.y;
        }
        if (this.FixedAxis.Z)
        {
            targetPosition.z = this.transform.position.z;
        }

        // Move
        if (this.LockedOn.Value)
        {
            this.transform.position = targetPosition;
        }
        else
        {
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, deltaTime * this.FollowSpeed.Value);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPosition : MonoBehaviour
{
    [Header("Settings")]
    public PositionType PositionType;
    public UpdateType UpdateType;

    [Header("Shared")]
    public Vector3Reference PositionVariable;

    void Update()
    {
        if (this.UpdateType == UpdateType.UPDATE)
        {
            this.UpdatePosition();
        }
    }

    void FixedUpdate()
    {
        if (this.UpdateType == UpdateType.FIXED_UPDATE)
        {
            this.UpdatePosition();
        }
    }

    void LateUpdate()
    {
        if (this.UpdateType == UpdateType.LATE_UPDATE)
        {
            this.UpdatePosition();
        }
    }
    
    private void UpdatePosition()
    {
        switch (this.PositionType)
        {
            case PositionType.LOCAL_POSITION:
                this.PositionVariable.Value = this.transform.localPosition;
                break;
            case PositionType.WORLD_POSITION:
                this.PositionVariable.Value = this.transform.position;
                break;
        }
    }
}


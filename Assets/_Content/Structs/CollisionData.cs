using System;
using UnityEngine;

[Serializable]
public struct CollisionData
{
    public GameObject Other;
    public ContactPoint[] Contacts;
}

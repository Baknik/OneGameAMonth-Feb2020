using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton : MonoBehaviour
{
    [HideInInspector]
    public bool Clicked;

    void Start()
    {
        this.Clicked = false;
    }

    public void OnClick()
    {
        this.Clicked = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    [Header("References")]
    public Button Button;

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

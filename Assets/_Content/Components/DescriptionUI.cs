using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionUI : MonoBehaviour
{
    [HideInInspector]
    public Text DescriptionText;

    void Awake()
    {
        this.DescriptionText = this.GetComponentInChildren<Text>();
    }
}

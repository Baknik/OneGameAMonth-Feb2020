using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildActionsUI : MonoBehaviour
{
    public ActionButton GunSatButton;
    public Text GunSatCostText;
    public ActionButton PulseSatButton;
    public Text PulseSatCostText;
    public ActionButton RamSatButton;
    public Text RamSatCostText;

    [Header("Settings")]
    public FloatReference GunSatBuildCost;
    public FloatReference PulseSatBuildCost;
    public FloatReference RamSatBuildCost;
}

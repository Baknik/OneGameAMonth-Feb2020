using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildActionsUI : MonoBehaviour
{
    public ActionButton GunSatButton;
    public Text GunSatCostText;
    public ActionButton ChillSatButton;
    public Text ChillSatCostText;
    public ActionButton RamSatButton;
    public Text RamSatCostText;
    public ActionButton LaserSatButton;
    public Text LaserSatCostText;

    [Header("Settings")]
    public FloatReference GunSatBuildCost;
    public FloatReference ChillSatBuildCost;
    public FloatReference RamSatBuildCost;
    public FloatReference LaserSatBuildCost;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageActionsUI : MonoBehaviour
{
    public ActionButton UpgradeButton;
    public Text UpgradeCostText;
    public ActionButton SlowOrbitSpeedButton;
    public Text SlowOrbitSpeedCostText;
    public ActionButton MediumOrbitSpeedButton;
    public Text MediumOrbitSpeedCostText;
    public ActionButton FastOrbitSpeedButton;
    public Text FastOrbitSpeedCostText;
    public ActionButton SellButton;
    public Text SellAmountText;
    public FloatReference OrbitSpeedChangeCost;
}

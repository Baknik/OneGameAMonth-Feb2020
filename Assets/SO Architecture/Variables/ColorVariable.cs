using UnityEngine;

[CreateAssetMenu(
    fileName = "ColorVariable.asset",
    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Color",
    order = SOArchitecture_Utility.ASSET_MENU_ORDER_VARIABLES)]
public class ColorVariable : BaseVariable<Color>
{
}
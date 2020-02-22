using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using UnityEngine.UI;

public class UISystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Selectable selectedObject = null;
        Entities.ForEach((Selectable selectable) =>
        {
            if (selectable.Selected)
            {
                selectedObject = selectable;
            }
        });
        Orbit selectedOrbit = null;
        Entities.ForEach((Orbit orbit, Selectable selectable) =>
        {
            if (selectedObject != null && selectable == selectedObject)
            {
                selectedOrbit = orbit;
            }
        });
        Money money = null;
        Entities.ForEach((Money m) =>
        {
            money = m;
        });

        // Title
        if (selectedObject != null)
        {
            Entities.ForEach((UITitle uiTitle, Text text) =>
            {
                if (selectedOrbit != null && selectedOrbit.Satellite != null)
                {
                    text.text = selectedOrbit.Satellite.Name;
                }
                else
                {
                    text.text = selectedObject.Name;
                }
            });
        }
        else
        {
            Entities.ForEach((UITitle uiTitle, Text text) =>
            {
                text.text = string.Empty;
            });
        }

        // Description
        if (selectedObject != null)
        {
            Entities.ForEach((DescriptionUI descriptionUI) =>
            {
                if (descriptionUI.DescriptionText != null)
                {
                    if (selectedOrbit != null && selectedOrbit.Satellite != null)
                    {
                        descriptionUI.DescriptionText.text = selectedOrbit.Satellite.Description;
                    }
                    else
                    {
                        descriptionUI.DescriptionText.text = selectedObject.Description;
                    }
                    descriptionUI.gameObject.SetActive(true);
                }
            });
        }
        else
        {
            Entities.ForEach((DescriptionUI descriptionUI) =>
            {
                if (descriptionUI.DescriptionText != null)
                {
                    descriptionUI.DescriptionText.text = string.Empty;
                    descriptionUI.gameObject.SetActive(false);
                }
            });
        }

        // Manage actions enabling
        Entities.ForEach((ManageActionsUI manageActionsUI) =>
        {
            if (selectedObject != null)
            {
                if (selectedOrbit != null)
                {
                    manageActionsUI.gameObject.SetActive(selectedOrbit.Satellite != null);
                }
                else
                {
                    manageActionsUI.gameObject.SetActive(false);
                }
            }
            else
            {
                manageActionsUI.gameObject.SetActive(false);
            }

            // Updates
            Entities.ForEach((Entity satelliteEntity, Satellite sat) =>
            {
                if (selectedOrbit != null && sat == selectedOrbit.Satellite)
                {
                    manageActionsUI.SellAmountText.text = $"Recieve ${this.GetFormattedNumberString(Mathf.Floor(sat.Value))}";
                }
            });

            // Button enabling
            // TODO
        });

        // Build actions enabling
        Entities.ForEach((BuildActionsUI buildActionsUI) =>
        {
            // Active
            if (selectedObject != null)
            {
                if (selectedOrbit != null)
                {
                    buildActionsUI.gameObject.SetActive(selectedOrbit.Satellite == null);
                }
                else
                {
                    buildActionsUI.gameObject.SetActive(false);
                }
            }
            else
            {
                buildActionsUI.gameObject.SetActive(false);
            }

            // Costs
            buildActionsUI.GunSatCostText.text = $"${this.GetFormattedNumberString(Mathf.Floor(buildActionsUI.GunSatBuildCost.Value))}";
            buildActionsUI.ChillSatCostText.text = $"${this.GetFormattedNumberString(Mathf.Floor(buildActionsUI.ChillSatBuildCost.Value))}";
            buildActionsUI.RamSatCostText.text = $"${this.GetFormattedNumberString(Mathf.Floor(buildActionsUI.RamSatBuildCost.Value))}";
            buildActionsUI.LaserSatCostText.text = $"${this.GetFormattedNumberString(Mathf.Floor(buildActionsUI.LaserSatBuildCost.Value))}";
        });

        // Manage actions actions
        Entities.ForEach((ManageActionsUI manageActionsUI) =>
        {
            // Manage
            if (manageActionsUI.SellButton.Clicked)
            {
                Entities.ForEach((Entity satelliteEntity, Satellite sat) =>
                {
                    if (sat == selectedOrbit.Satellite)
                    {
                        money.CurrentMoney += sat.Value;
                        selectedOrbit.Satellite = null;
                        PostUpdateCommands.DestroyEntity(satelliteEntity);
                        GameObject.Destroy(sat.gameObject);
                    }
                });
                manageActionsUI.SellButton.Clicked = false;
            }
        });

        // Build actions actions
        Entities.ForEach((BuildActionsUI buildActionsUI) =>
        {
            // Build
            string prefabName = null;
            float satStartValue = 0f;
            if (buildActionsUI.GunSatButton.Clicked)
            {
                if (money.CurrentMoney >= buildActionsUI.GunSatBuildCost.Value)
                {
                    prefabName = "GunSatellite";
                    money.CurrentMoney -= buildActionsUI.GunSatBuildCost.Value;
                    satStartValue = buildActionsUI.GunSatBuildCost.Value;
                }
                buildActionsUI.GunSatButton.Clicked = false;
            }
            else if (buildActionsUI.ChillSatButton.Clicked)
            {
                if (money.CurrentMoney >= buildActionsUI.ChillSatBuildCost.Value)
                {
                    prefabName = "ChillSatellite";
                    money.CurrentMoney -= buildActionsUI.ChillSatBuildCost.Value;
                    satStartValue = buildActionsUI.ChillSatBuildCost.Value;
                }
                buildActionsUI.ChillSatButton.Clicked = false;
            }
            else if (buildActionsUI.RamSatButton.Clicked)
            {
                if (money.CurrentMoney >= buildActionsUI.RamSatBuildCost.Value)
                {
                    prefabName = "RamSatellite";
                    money.CurrentMoney -= buildActionsUI.RamSatBuildCost.Value;
                    satStartValue = buildActionsUI.RamSatBuildCost.Value;
                }
                buildActionsUI.RamSatButton.Clicked = false;
            }
            else if (buildActionsUI.LaserSatButton.Clicked)
            {
                if (money.CurrentMoney >= buildActionsUI.LaserSatBuildCost.Value)
                {
                    prefabName = "LaserSatellite";
                    money.CurrentMoney -= buildActionsUI.LaserSatBuildCost.Value;
                    satStartValue = buildActionsUI.LaserSatBuildCost.Value;
                }
                buildActionsUI.LaserSatButton.Clicked = false;
            }
            if (prefabName != null)
            {
                GameObject satObject = PrefabFactory.Instance.InstantiatePrefab(prefabName, new Vector3(selectedOrbit.Radius, 0f, 0f), Quaternion.Euler(0f, 90f, 0f), null);
                Satellite sat = satObject.GetComponent<Satellite>();
                if (sat != null)
                {
                    sat.Value = satStartValue;
                    selectedOrbit.Satellite = sat;
                }
            }
        });

        // Health
        Entities.ForEach((HealthUI healthUI) =>
        {
            healthUI.gameObject.SetActive(false);
        });
        Entities.ForEach((Selectable selectable, Health health) =>
        {
            if (selectable.Selected)
            {
                Entities.ForEach((HealthUI healthUI) =>
                {
                    if (healthUI.HealthSlider != null)
                    {
                        healthUI.HealthSlider.maxValue = health.MaxHealth;
                        healthUI.HealthSlider.value = health.CurrentHealth;
                    }

                    if (healthUI.HealthText != null)
                    {
                        healthUI.HealthText.text =
                            $"{this.GetFormattedNumberString(Mathf.Floor(health.CurrentHealth))}/{this.GetFormattedNumberString(Mathf.Floor(health.MaxHealth))}";
                    }

                    healthUI.gameObject.SetActive(true);
                });
            }
        });

        // Object attached UI
        Entities.ForEach((Entity entity, ObjectAttachedUI objectAttachedUI, RectTransform rectTransform) =>
        {
            if (objectAttachedUI.Object != null)
            {
                rectTransform.position = objectAttachedUI.Object.transform.position + objectAttachedUI.Offset;
            }
            else
            {
                PostUpdateCommands.DestroyEntity(entity);
                GameObject.Destroy(objectAttachedUI.gameObject);
            }
        });

        // Money
        if (money != null)
        {
            Entities.ForEach((MoneyUI moneyUI) =>
            {
                moneyUI.MoneyText.text = $"${this.GetFormattedNumberString(Mathf.Floor(money.CurrentMoney))}";
            });
        }
     }

    private string GetFormattedNumberString(float number)
    {
        return number.ToString("n0");
    }
}

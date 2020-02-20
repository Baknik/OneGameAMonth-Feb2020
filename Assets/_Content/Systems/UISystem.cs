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
                    descriptionUI.DescriptionText.text = selectedObject.Description;
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

        // Build actions
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

            // Build
            string prefabName = null;
            if (buildActionsUI.GunSatButton.Clicked)
            {
                prefabName = "GunSatellite";
                buildActionsUI.GunSatButton.Clicked = false;
            }
            else if (buildActionsUI.ChillSatButton.Clicked)
            {
                prefabName = "ChillSatellite";
                buildActionsUI.ChillSatButton.Clicked = false;
            }
            else if (buildActionsUI.RamSatButton.Clicked)
            {
                prefabName = "RamSatellite";
                buildActionsUI.RamSatButton.Clicked = false;
            }
            else if (buildActionsUI.LaserSatButton.Clicked)
            {
                prefabName = "LaserSatellite";
                buildActionsUI.LaserSatButton.Clicked = false;
            }
            if (prefabName != null)
            {
                GameObject satObject = PrefabFactory.Instance.InstantiatePrefab(prefabName, new Vector3(selectedOrbit.Radius, 0f, 0f), Quaternion.identity, null);
                Satellite sat = satObject.GetComponent<Satellite>();
                if (sat != null)
                {
                    selectedOrbit.Satellite = sat;
                }
            }
        });

        // Manage actions
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
    }

    private string GetFormattedNumberString(float number)
    {
        return number.ToString("n0");
    }
}

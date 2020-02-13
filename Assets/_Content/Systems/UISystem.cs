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

        // Title
        if (selectedObject != null)
        {
            Entities.ForEach((UITitle uiTitle, Text text) =>
            {
                text.text = selectedObject.Name;
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
                        healthUI.HealthText.text = $"{health.CurrentHealth}/{health.MaxHealth}";
                    }

                    healthUI.gameObject.SetActive(true);
                });
            }
        });
    }
}

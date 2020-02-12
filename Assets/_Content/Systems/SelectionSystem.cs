using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class SelectionSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        InputCollector input = null;
        Camera camera = null;

        Entities.ForEach((InputCollector i) =>
        {
            input = i;
        });

        Entities.ForEach((ViewCamera vc, Camera c) =>
        {
            camera = c;
        });

        Selectable hoverObject = null;
        if (camera != null)
        {
            Ray mouseRay = camera.ScreenPointToRay(input.MouseScreenPosition);
            RaycastHit[] hits = Physics.RaycastAll(mouseRay, 100f);
            
            Entities.ForEach((Selectable selectable) =>
            {
                // Cancel current hover
                selectable.Hovering = false;

                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider == selectable.Collider)
                    {
                        if (hoverObject == null)
                        {
                            hoverObject = selectable;
                        }
                        else if (selectable.SelectionPriority >= hoverObject.SelectionPriority)
                        {
                            hoverObject = selectable;
                        }
                        break;
                    }
                }
            });
        }

        if (hoverObject != null)
        {
            hoverObject.Hovering = true;
        }

        if (input != null)
        {
            if (input.SelectInput)
            {
                input.SelectInput = false;

                // Cancel current selection
                Entities.ForEach((Selectable selectable) =>
                {
                    selectable.Selected = false;
                });

                if (hoverObject != null)
                {
                    hoverObject.Selected = true;
                }
            }
        }
    }
}

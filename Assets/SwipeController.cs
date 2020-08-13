using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private ButtonSwitch[] switches;
    private Canvas canvas;

    private void Awake()
    {
        switches = GetComponentsInChildren<ButtonSwitch>();
        canvas = GetComponent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"drag position = {eventData.position}, drag world position = {eventData.pointerCurrentRaycast.worldPosition}, drag screen position = {eventData.pointerCurrentRaycast.screenPosition}");
        return;
        foreach (var sw in switches)
        {
            // Камера, на которой работает канвас, переведи, пожалуйста, позицию свайпа из экранных координат в координаты игрового мира

            var dragPos = canvas.worldCamera.ScreenToWorldPoint(eventData.position);
            
            // Если позиция свайпа попадает в зону свайпа переключателя

            var xOK = (dragPos.x >= sw.SwipeZoneCentr.x - sw.SwipeZoneSize.x / 2) && (dragPos.x <= sw.SwipeZoneCentr.x + sw.SwipeZoneSize.x / 2);
            var yOK = (dragPos.y >= sw.SwipeZoneCentr.y - sw.SwipeZoneSize.y / 2) && (dragPos.y <= sw.SwipeZoneCentr.y + sw.SwipeZoneSize.y / 2);
            
            if (sw.name == "RailwaySwitch (1)")
            {
                //Debug.Log($"{dragPos.x} >= {sw.SwipeZoneCentr.x} - {sw.SwipeZoneSize.x / 2}", this);
                //Debug.Log($"{dragPos.x} <= {sw.SwipeZoneCentr.x} + {sw.SwipeZoneSize.x / 2}", this);
            }

            if (xOK && yOK)
            {
                // Если свайп "по горизонтали"

                if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
                {
                    // Если смещение вправо

                    if (eventData.delta.x > 0)
                    {
                        sw.SwitchToRight();
                    }

                    // Если смещение влево

                    else
                    {
                        sw.SwitchToLeft();
                    }
                }
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
}

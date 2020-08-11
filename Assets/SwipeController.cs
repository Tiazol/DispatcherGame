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
        foreach (var sw in switches)
        {
            var dragPos = canvas.worldCamera.ScreenToWorldPoint(eventData.position); // для справки: центр координат eventData.position находится внизу слева
            var switchPos = new Vector3(sw.transform.position.x, sw.transform.position.y, sw.transform.position.z - 5);

            // Если расстояние между нажатием и кнопкой меньше указанного

            if (Vector2.Distance(dragPos, switchPos) < 1f)
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

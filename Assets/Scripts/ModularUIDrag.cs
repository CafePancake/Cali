using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ModularUIDrag : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    Vector2 screenBounds;
    [SerializeField]float wMargin;
    [SerializeField]float hMargin;
    void Awake()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    public void DragHandler(BaseEventData data)
    {
        PointerEventData pointerdata = (PointerEventData)data;
        Vector2 position;

        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform,pointerdata.position,canvas.worldCamera, out position);
        transform.position=canvas.transform.TransformPoint(position);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -screenBounds.x +wMargin, screenBounds.x -wMargin), Mathf.Clamp(transform.position.y, -screenBounds.y +hMargin, screenBounds.y -hMargin));
        // Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // transform.position = cursorPos;
    }
}

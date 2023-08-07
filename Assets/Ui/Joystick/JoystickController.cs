using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static UnityEditor.Experimental.GraphView.GraphView;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public JoystickMovedEvent joystickMoved;
    public RectTransform lever;
    public RectTransform joystickRectTransform;

    private void Start()
    {
        Assert.IsNotNull(lever);
        Assert.IsNotNull(joystickRectTransform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetLeverPosition(eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetLeverPosition(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SetLeverPosition(joystickRectTransform.position);
    }

    private void SetLeverPosition(Vector2 newPosition)
    {
        lever.position = newPosition;
        var joystickRadius = joystickRectTransform.rect.width / 2;
        lever.localPosition = Vector2.ClampMagnitude(lever.localPosition, joystickRadius);
        var joystickPosition = lever.localPosition / joystickRadius;
        joystickMoved.Invoke(joystickPosition);
        print(joystickPosition);
    }
}

[Serializable]
public class JoystickMovedEvent: UnityEvent<Vector2> { }

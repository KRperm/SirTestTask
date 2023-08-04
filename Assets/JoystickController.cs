using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class JoystickController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public JoystickMovedEvent JoystickMoved;
    public RectTransform Lever;

    private RectTransform RectTransform { get; set; }
    private float JoystickRadius { get; set; }

    private void Start()
    {
        Assert.IsNotNull(Lever);
        RectTransform = GetComponent<RectTransform>();
        Assert.IsNotNull(RectTransform);
        JoystickRadius = RectTransform.rect.width / 2;
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
        SetLeverPosition(RectTransform.position);
    }

    private void SetLeverPosition(Vector2 newPosition)
    {
        Lever.position = Vector2.MoveTowards(RectTransform.position, newPosition, JoystickRadius);
        var joystickPosition = Lever.localPosition / JoystickRadius;
        JoystickMoved.Invoke(joystickPosition);
    }
}

[Serializable]
public class JoystickMovedEvent: UnityEvent<Vector2> { }

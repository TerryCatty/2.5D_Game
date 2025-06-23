using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonInput : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent onDown;

    public void OnPointerDown(PointerEventData eventData)
    {
        onDown?.Invoke();
    }

}

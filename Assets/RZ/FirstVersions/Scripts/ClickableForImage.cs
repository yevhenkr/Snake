using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[DisallowMultipleComponent]
public class ClickableForImage : MonoBehaviour, IPointerClickHandler
{
    public UnityEngine.Events.UnityEvent onClick = new UnityEngine.Events.UnityEvent();

    public void OnPointerClick(PointerEventData eventData) { onClick.Invoke(); }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class mobilePurchaseButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool _pressed=false;
    public virtual void OnPointerDown(PointerEventData ped)
    {
        _pressed = true;
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        _pressed = false;
    }
}

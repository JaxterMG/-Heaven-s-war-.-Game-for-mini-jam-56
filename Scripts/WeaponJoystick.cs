using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image _joystickBG;
    private Image _joystick;
    private Vector2 _inputVec2;
    public bool _shooting = false; 
    private void Start()
    {
        _joystickBG = GetComponent<Image>();
        _joystick = transform.GetChild(0).GetComponent<Image>();
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
        _shooting = true;
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        _inputVec2 = Vector2.zero;
        _joystick.rectTransform.anchoredPosition = Vector2.zero;
        _shooting = false;
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBG.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / _joystickBG.rectTransform.sizeDelta.x);
            pos.y = (pos.y / _joystickBG.rectTransform.sizeDelta.x);
            _inputVec2 = new Vector2(pos.x, pos.y);
            _inputVec2 = (_inputVec2.magnitude > 1.0f) ? _inputVec2.normalized : _inputVec2;
            _joystick.rectTransform.anchoredPosition = new Vector2(_inputVec2.x * (_joystickBG.rectTransform.sizeDelta.x / 2), _inputVec2.y * (_joystickBG.rectTransform.sizeDelta.y / 2));
        }
    }
    public float Horizontal()
    {
        return _inputVec2.x;

    }
    public float Vertical()
    {
        return _inputVec2.y;
    }
    public Vector2 GetVec2()
    {
        return _inputVec2;
    }    
}


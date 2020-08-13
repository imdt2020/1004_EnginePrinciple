using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class JoystickInputBehaviour : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image background, joystick;     //摇杆的背景图片和摇杆图片。
    public Vector2 value;                  //摇杆的值

    public event Action<Vector2> onValueChange;
    public float GetAxisX() { return value.x; }
    public float GetAxisY() { return value.y; }
    public Vector2 GetAxisValue() { return value; }


    // Use this for initialization
    void Start()
    {
        background = this.GetComponent<Image>();
        joystick = this.transform.GetChild(0).GetComponent<Image>();
    }

    /// <summary>
    /// 当拖动发生时，在拖动中每一次移动都会调用该函数
    /// </summary>
    /// <param name="ped"></param>
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;

        //将当前鼠标的坐标值从屏幕空间转换为相对摇杆背景的坐标空间
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            // 归一化当前鼠标在背景图中的坐标
            pos.x = (pos.x / background.rectTransform.sizeDelta.x);
            pos.y = (pos.y / background.rectTransform.sizeDelta.y);

            // Position is relative to the analog base.
            value = pos * 3;// new Vector3(pos.x * 2 - 1, pos.y * 2 - 1);
            value = (value.magnitude > 1.0f ? value.normalized : value);

            // Move the virtual analog stick based on pointer position;
            joystick.rectTransform.anchoredPosition =
                new Vector3(value.x * (background.rectTransform.sizeDelta.x /3),
                            value.y * (background.rectTransform.sizeDelta.y /3));

            if(onValueChange != null)
            {
                onValueChange.Invoke(value);
            }
            
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        value = Vector3.zero;
        joystick.rectTransform.anchoredPosition = Vector3.zero;

        if (onValueChange != null)
        {
            onValueChange.Invoke(value);
        }
    }

}

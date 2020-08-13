using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ButtonInputBehaviour : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private Image button;
    public bool pressed;
    public bool pressedUp;
    public bool pressedDown;
    public int frames;

    public Action onPressed;
    public Action onPressedDown;
    public Action onPressedUp;

    // Use this for initialization
    void Start()
    {
        button = this.GetComponent<Image>();
    }

    /// <summary>
    /// 当按钮被点击时，会调用该函数 
    /// </summary>
    /// <param name="ped"></param>
    public virtual void OnPointerDown(PointerEventData ped)
    {
        pressed = true;
    }

    
    public virtual void OnPointerUp(PointerEventData ped)
    {
        pressed = false;
    }


    public bool IsPressed() { return pressed; }
    public bool IsUp() { return pressedUp; }
    public bool IsDown() { return pressedDown; }

    // Update is called once per frame
    void Update()
    {
        if (pressed)
        {
            if (pressedDown)
            {
                pressedDown = false;
            }
            else if (frames == 0)
            {
                pressedDown = true;
                if (onPressedDown != null) onPressedDown.Invoke();
                Debug.Log("Action<" + this.gameObject.name + "> PressedDown:" + pressedDown);
            }
            frames++;

            if(onPressed!=null) onPressed.Invoke();
        }
        else
        {
            if (pressedUp)
            {
                pressedUp = false;
            }
            else if (frames != 0)
            {
                pressedUp = true;
                if (onPressedUp != null) onPressedUp.Invoke();
                Debug.Log("Action<" + this.gameObject.name + "> PressedUp:" + pressedUp);
            }
            frames = 0;
            pressedDown = false;
        }
    }
}

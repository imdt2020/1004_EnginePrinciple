using UnityEngine;
using System.Collections.Generic;
using System;

public class RPGInput : MonoBehaviour
{
    public Dictionary<string, ButtonInputBehaviour> buttons = new Dictionary<string, ButtonInputBehaviour>();
    public Dictionary<string, JoystickInputBehaviour> joysticks = new Dictionary<string, JoystickInputBehaviour>();

    
    // Use this for initialization
    void Awake()
    {
        //记录所有的Action输入器
        {
            ButtonInputBehaviour[] inputs = this.gameObject.GetComponentsInChildren<ButtonInputBehaviour>();
            foreach (ButtonInputBehaviour input in inputs)
            {
                buttons.Add(input.gameObject.name, input);
            }
        }

        //记录所有的Joystick输入器
        {
            JoystickInputBehaviour[] inputs = this.gameObject.GetComponentsInChildren<JoystickInputBehaviour>();
            foreach (JoystickInputBehaviour input in inputs)
            {
                joysticks.Add(input.gameObject.name, input);
            }
        }

    }

    public void BindJoystick(string name, Action<Vector2> listener)
    {
        if (joysticks.ContainsKey(name))
        {
            joysticks[name].onValueChange += listener;
        }
    }

    public void UnBindJoystick(string name, Action<Vector2> listener)
    {
        if (joysticks.ContainsKey(name))
        {
            joysticks[name].onValueChange -= listener;
        }
    }

    public void BindButton(string name, Action pressedListener, Action pressedDownListener, Action pressedUpListener)
    {
        if (buttons.ContainsKey(name))
        {
            buttons[name].onPressed += pressedListener;
            buttons[name].onPressedDown += pressedDownListener;
            buttons[name].onPressedUp += pressedUpListener;
        }
    }

    public void UnBindButton(string name, Action pressedListener, Action pressedDownListener, Action pressedUpListener)
    {
        if (buttons.ContainsKey(name))
        {
            buttons[name].onPressed -= pressedListener;
            buttons[name].onPressedDown -= pressedDownListener;
            buttons[name].onPressedUp -= pressedUpListener;
        }
    }


    public Vector2 GetJoystickValue(string name)
    {
        if(joysticks.ContainsKey(name))
        {
            return joysticks[name].GetAxisValue();
        }
        return Vector2.zero;
    }

    public bool IsButtonPressed(string name)
    {
        if(buttons.ContainsKey(name))
        {
            return buttons[name].IsPressed();
        }
        return false;
    }

    public bool IsButtonUp(string name)
    {
        if (buttons.ContainsKey(name))
        {
            return buttons[name].IsUp();
        }
        return false;
    }

    public bool IsButtonDown(string name)
    {
        if (buttons.ContainsKey(name))
        {
            return buttons[name].IsDown();
        }
        return false;
    }
}

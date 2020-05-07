using System.Collections.Generic;


public static class VirtualInput
{
	private static Dictionary<string, VirtualAnalog> analogs = new Dictionary<string, VirtualAnalog>();
	private static Dictionary<string, VirtualButton> buttons = new Dictionary<string, VirtualButton>();

	public static void AddAxis(string axisName, VirtualAnalog analog)
	{
		if (!analogs.ContainsKey(axisName))
		{
			analogs.Add(axisName, analog);
		}
		else
		{
			analogs[axisName] = analog;
		}
	}

	public static bool RemoveAxis(string axisName)
	{
		return analogs.Remove(axisName);
	}

	
	public static void AddButton(string buttonName, VirtualButton button)
	{
		if (!buttons.ContainsKey(buttonName))
		{
			buttons.Add(buttonName, button);
		}
		else
		{
			buttons[buttonName] = button;
		}
	}

	
	public static bool RemoveButton(string buttonName)
	{
		return buttons.Remove(buttonName);
	}


	public static float GetAxis(string axisName)
	{
        if (analogs.ContainsKey(axisName) && analogs[axisName].GetAxis(axisName) != 0)
        {
            return analogs[axisName].GetAxis(axisName);
        }

		return 0;
	}

	
	public static float GetAxisRaw(string axisName)
	{
        if (analogs.ContainsKey(axisName) && analogs[axisName].GetAxisRaw(axisName) != 0)
        {
            return analogs[axisName].GetAxisRaw(axisName);
        }
        else if (buttons.ContainsKey(axisName))
        {
            return buttons[axisName].GetRaw();
        }
		return 0;
	}

	
	public static bool GetButton(string buttonName)
	{
        if (buttons.ContainsKey(buttonName) && buttons[buttonName].Get())
        {
            return true;
        }
        else if (analogs.ContainsKey(buttonName) && analogs[buttonName].GetButton(buttonName))
        {
            return true;
        }
		return false;
	}

	
	public static bool GetButtonDown(string buttonName)
	{
        if (buttons.ContainsKey(buttonName) && buttons[buttonName].GetDown())
        {
            return true;
        }
		return false;
	}

	
	public static bool GetButtonUp(string buttonName)
	{
        if (buttons.ContainsKey(buttonName) && buttons[buttonName].GetUp())
        {
            return true;
        }
		return false;
	}

	
	public static bool GetKey(string name)
	{
        if (buttons.ContainsKey(name))
        {
            return buttons[name].Get();
        }
		return false;
	}

	
	public static bool GetKeyDown(string name)
	{
        if (buttons.ContainsKey(name))
        {
            return buttons[name].GetDown();
        }
		return false;
	}

	
	public static bool GetKeyUp(string name)
	{
        if (buttons.ContainsKey(name))
        {
            return buttons[name].GetUp();
        }
		return false;
	}


	public static List<string> GetActiveAnalogs()
	{
		var analogIds = new List<string>();
		foreach (KeyValuePair<string, VirtualAnalog> entry in analogs)
		{
			if (entry.Value.GetId >= 0)
			{
				analogIds.Add(entry.Value.GetId.ToString());
			}
		}
		return analogIds;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LPPV_VButton : MonoBehaviour {

	public enum ButtonType
	{
		TriggerButton, HoldButton
	}

	public ButtonType buttonType;

	[HideInInspector]
	public bool value;

	private EventTrigger _eventTrigger;

	private void Start()
	{
		SetEventTrigger ();
	}

	private void SetEventTrigger()
	{
		if (GetComponent<EventTrigger> ()) {
			_eventTrigger = GetComponent<EventTrigger> ();
		} else {
			gameObject.AddComponent<EventTrigger> ();
			_eventTrigger = GetComponent<EventTrigger> ();
		}

		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener((eventData) => {OnButtonDown();});
		_eventTrigger.triggers = new List<EventTrigger.Entry> ();
		_eventTrigger.triggers.Add(entry);


		EventTrigger.Entry entry1 = new EventTrigger.Entry();
		entry1.eventID = EventTriggerType.PointerUp;
		entry1.callback.AddListener((eventData) => {OnButtonUp();});
		_eventTrigger.triggers.Add(entry1);
	}

	private void Update()
	{
		if (buttonType == ButtonType.TriggerButton) 
		{
			if (value)
				value = false;
		}
	}

	public void OnButtonDown()
	{
		value = true;
	}

	public void OnButtonUp()
	{
		value = false;
	}
}

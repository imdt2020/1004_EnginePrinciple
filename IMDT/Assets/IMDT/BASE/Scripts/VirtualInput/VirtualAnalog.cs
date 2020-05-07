using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class VirtualAnalog : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
	public string inputXAxis;                // The X axis of the virtual analog.
	public bool invertX;                     // Bollean to define whether or not the X axis is inverted.
	public string inputYAxis;                // The Y axis of the virtual analog.
	public bool invertY;                     // Bollean to define whether or not the Y axis is inverted.
	public float dead = 0.02f;               // Size of the virtual analog dead zone.
	public float sensitivity = 1;            // Speed to move towards target value.
	public string buttonName;                // Name of the virtual button (simulating the pressing analog action).
	public Color pressColor;                 // Color of the analog when pressed.

	private Image bg, joystick;              // The image references for the virtual analog base and stick.
	private Vector2 stickPosition;           // The vector containg the virtual analog stick current position.
	private bool pressed;                    // Bollean to define whether or not the analog is being pressed.
	private Color releaseColor;              // The color of the virtual analog when released (original color).
	private float doubleClickTimer;          // A timer variable to define if a double click action has occurred.
	private float doubleClickDelay = 0.4f;   // The interval between button presses to define the double click action.
	private int id;

	public int GetId { get { return id; } }

	
	void Start()
	{
		doubleClickTimer = 0;
		bg = this.GetComponent<Image>();
		joystick = this.transform.GetChild(0).GetComponent<Image>();
		releaseColor = joystick.color;

		// Subscribe this virtual analog axes on the virtual Input manager.
		VirtualInput.AddAxis(inputXAxis, this);
		VirtualInput.AddAxis(inputYAxis, this);
		VirtualInput.AddAxis(buttonName, this);
		id = -100;
	}

	// When draging is occuring this will be called every time the cursor is moved.
	public virtual void OnDrag(PointerEventData ped)
	{
		id = ped.pointerId;
		Vector2 pos;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bg.rectTransform, ped.position, ped.pressEventCamera, out pos))
		{
			// Get the analog position on the 2 axes based on pointer position.
			pos.x = (pos.x / bg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bg.rectTransform.sizeDelta.y);

			// Position is relative to the analog base.
			stickPosition = new Vector3(pos.x * 2 + 1, pos.y * 2 - 1);
			stickPosition = (stickPosition.magnitude > 1.0f ? stickPosition.normalized : stickPosition);

			// Move the virtual analog stick based on pointer position;
			joystick.rectTransform.anchoredPosition =
				new Vector3(stickPosition.x * (bg.rectTransform.sizeDelta.x / 3),
							stickPosition.y * (bg.rectTransform.sizeDelta.y / 3));
		}
		// Apply the final analog sensitivity.
		stickPosition *= sensitivity;
	}

	// When the virtual analog's press occured this will be called.
	public virtual void OnPointerDown(PointerEventData ped)
	{
		// Only keep pressed when a double click action is detected.
		if (doubleClickTimer > 0 && (Time.time - doubleClickTimer) < doubleClickDelay)
		{
			joystick.color = pressColor;
			pressed = true;
			doubleClickTimer = 0;
		}
		doubleClickTimer = Time.time;
		OnDrag(ped);
	}

	// When the virtual analog's release occured this will be called.
	public virtual void OnPointerUp(PointerEventData ped)
	{
		joystick.color = releaseColor;
		pressed = false;
		stickPosition = Vector3.zero;
		joystick.rectTransform.anchoredPosition = Vector3.zero;
		id = -100;
	}

	// Returns true while the this virtual analog identified by name is pressed and held down.
	public bool GetButton(string name)
	{
		if (name == buttonName)
			return pressed;
		return false;
	}

	// Returns the value of this analog's virtual axis identified by axisName.
	public float GetAxis(string axisName)
	{
		if (stickPosition.magnitude <= dead)
			return 0;
		else if (axisName == inputXAxis)
			return invertX ? -stickPosition.x : stickPosition.x;
		else if (axisName == inputYAxis)
			return invertY ? -stickPosition.y : stickPosition.y;
		else
			return 0;
	}

	// Returns the value of this analog's virtual axis identified by axisName with no smoothing filtering applied.
	public float GetAxisRaw(string axisName)
	{
		if (axisName == inputXAxis)
			return GetRaw(invertX ? -stickPosition.x : stickPosition.x);
		else if (axisName == inputYAxis)
			return GetRaw(invertY ? -stickPosition.y : stickPosition.y);
		else if (axisName == buttonName)
			return pressed ? 1 : 0;
		else
			return 0;
	}

	// Convert axis smoothed to raw value.
	private float GetRaw(float value)
	{
		if (value > 0)
			return 1;
		else if (value < 0)
			return -1;
		else
			return 0;
	}
}

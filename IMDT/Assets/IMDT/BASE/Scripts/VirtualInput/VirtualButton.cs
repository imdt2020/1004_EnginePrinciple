using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class VirtualButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
	public string inputName;                // Name of the virtual button.
	public Color pressColor;                // Color of the button when pressed.

	private Image button;                   // The image reference for the virtual button.
	private Color releaseColor;             // The color of the virtual button when released (original color).
	private bool pressed;                   // Bollean to define whether or not the button is being pressed.
	private bool pressedDown;               // Bollean to define the frame the user pressed down the virtual button.
	private bool pressedUp;                 // Boolean to define the frame the user releases the virtual button.
	private int frames;                     // Frame count while the virtual button is pressed.
	private int step;

	
	void Start()
	{
		button = this.GetComponent<Image>();
		releaseColor = button.color;

		VirtualInput.AddButton(inputName, this);
	}

	void Update()
	{
		// Ensure the pressed down action (true only in 1 frame).
		if(pressed)
		{
            if (pressedDown)
            {
                pressedDown = false;
            }
            else if (frames == 0)
            {
                pressedDown = true;
            }
			frames++;
		}
		// Ensure the pressed up action (true only in 1 frame).
		else
		{
            if (pressedUp)
            {
                pressedUp = false;
            }
            else if (frames != 0)
            {
                pressedUp = true;
            }
			frames = 0;
			pressedDown = false;
		}
	}

	// When the virtual button's press occured this will be called.
	public virtual void OnPointerDown(PointerEventData ped)
	{
		button.color = pressColor;
		pressed = true;
	}

	// When the virtual button's release occured this will be called.
	public virtual void OnPointerUp(PointerEventData ped)
	{
		button.color = releaseColor;
		pressed = false;
	}

	// Returns true the first frame the user releases this virtual button.
	public bool GetUp()
	{
		return pressedUp;
	}

	// Returns true the first frame the user presses this virtual button.
	public bool GetDown()
	{
		return pressedDown;
	}

	// Returns true while the this virtual button is pressed and held down.
	public bool Get()
	{
		return pressed;
	}

	// Simulate a raw input for this virtual button.
	public int GetRaw()
	{
		return pressed ? 1 : 0;
	}
}

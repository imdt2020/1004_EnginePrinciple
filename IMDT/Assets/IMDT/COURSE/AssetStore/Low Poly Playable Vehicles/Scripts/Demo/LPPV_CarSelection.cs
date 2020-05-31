using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LPPV_CarSelection : MonoBehaviour {
	
	public static CarType currentCarType;

	[SerializeField] private GameObject nextButton, prevButton;
	[SerializeField] private Camera cam;
	private int currentCar = 0;

	public enum CarType
	{
		Sedan, Sports, Utility, Bus
	}
		
	private void CheckStatus()
	{
		currentCar = Mathf.Clamp (currentCar, 0, 3);
		if (nextButton == null || prevButton == null)
			return;
		
		if (currentCar <= 0) 
		{
			prevButton.SetActive (false);
			nextButton.SetActive (true);
		} else if (currentCar >= 3) 
		{
			prevButton.SetActive (true);
			nextButton.SetActive (false);
		} else 
		{
			prevButton.SetActive (true);
			nextButton.SetActive (true);
		}
		
	}

	private void Start()
	{
		if (nextButton != null) 
		{
			if(nextButton.GetComponent<Button>())
				nextButton.GetComponent<Button> ().onClick.AddListener (() => { currentCar++; if(cam != null) cam.transform.position = new Vector3(cam.transform.position.x + 20f, cam.transform.position.y, cam.transform.position.z);} );
		}
		if (prevButton != null) 
		{
			if(prevButton.GetComponent<Button>())
				prevButton.GetComponent<Button> ().onClick.AddListener (() => { currentCar--; if(cam != null) cam.transform.position = new Vector3(cam.transform.position.x - 20f, cam.transform.position.y, cam.transform.position.z); } );
		}
		CheckStatus ();
	}

	private void Update()
	{
		CheckStatus ();
	}

	public void SelectCar()
	{
		currentCarType = (CarType)currentCar;
		SceneManager.LoadScene (1);
	}
}

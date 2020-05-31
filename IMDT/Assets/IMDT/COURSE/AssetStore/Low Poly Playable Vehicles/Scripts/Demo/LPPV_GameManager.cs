using UnityEngine.SceneManagement;
using UnityEngine;

public class LPPV_GameManager : MonoBehaviour 
{
	[SerializeField] private GameObject sedan, sports, utility, bus;
	private void Start()
	{
		if (sedan == null || sports == null || utility == null || bus == null)
			return;
		
		sedan.SetActive (false);
		sports.SetActive(false);
		utility.SetActive(false);
		bus.SetActive(false);

		if (LPPV_CarSelection.currentCarType == LPPV_CarSelection.CarType.Sedan)
			sedan.SetActive (true);
		else if (LPPV_CarSelection.currentCarType == LPPV_CarSelection.CarType.Sports)
			sports.SetActive (true);
		else if (LPPV_CarSelection.currentCarType == LPPV_CarSelection.CarType.Utility)
			utility.SetActive (true);
		else if (LPPV_CarSelection.currentCarType == LPPV_CarSelection.CarType.Bus)
			bus.SetActive (true);
	}

	public void LoadLevel(int index)
	{
		SceneManager.LoadScene (index);
	}
}

using BzKovSoft.RagdollTemplate.Scripts.Charachter;
using UnityEngine;

namespace BzKovSoft.RagdollTemplate.Scripts.Tools
{
	/// <summary>
	/// Display the health level on top of screen
	/// </summary>
	public sealed class BzDisplayHealth : MonoBehaviour
	{
		[SerializeField]
		BzHealth _bzHealth;
		GUIStyle _labelStile;

		// Update is called once per frame
		void OnGUI()
		{
			if (_bzHealth == null)
				return;

			if (_labelStile == null)
			{
				_labelStile = GUI.skin.GetStyle("Label");
				_labelStile.alignment = TextAnchor.UpperCenter;
			}
			GUI.Label(new Rect((Screen.width - 100) / 2, 10, 100, 100), "Health: " + (_bzHealth.Health * 100).ToString("N0"), _labelStile);
		}
	}
}
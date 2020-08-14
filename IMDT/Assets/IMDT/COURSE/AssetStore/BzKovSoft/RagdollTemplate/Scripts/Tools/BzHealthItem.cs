using BzKovSoft.RagdollTemplate.Scripts.Charachter;
using UnityEngine;

namespace BzKovSoft.RagdollTemplate.Scripts.Tools
{
	/// <summary>
	/// Script have to be attached to Health Items
	/// </summary>
	public sealed class BzHealthItem : MonoBehaviour
	{
		/// <summary>
		/// The amount of health in item
		/// </summary>
		[SerializeField]
		float _addHealth = 0.25f;

		// Use this for initialization
		void OnTriggerEnter(Collider collider)
		{
			IBzDamageable damageable = collider.GetComponent<IBzDamageable>();
			if (damageable == null)
				return;

			if (damageable.IsFullHealth())
				return;

			damageable.Health += _addHealth;
			Destroy(gameObject);
		}
	}
}
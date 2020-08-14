using UnityEngine;

namespace BzKovSoft.RagdollTemplate.Scripts.Charachter
{
	public interface IBzRagdollCharacter
	{
		/// <summary>
		/// Character current velocity.
		/// </summary>
		Vector3 CharacterVelocity { get; }

		/// <summary>
		/// Turn off controller script
		/// </summary>
		/// <param name="enable">Turn off if False</param>
		void CharacterEnable(bool enable);
	}
}
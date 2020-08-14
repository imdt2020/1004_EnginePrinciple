using UnityEngine;

namespace BzKovSoft.RagdollTemplate.Scripts.Charachter
{
	public sealed class BzHealth : MonoBehaviour, IBzDamageable
	{
		[SerializeField]
		IBzRagdoll _bzRagdoll;

		float _health = 1f;
		public float Health
		{
			get { return _health; }
			set
			{
				if (_health > Mathf.Epsilon && value <= Mathf.Epsilon)
				{
					if (_bzRagdoll != null)
						_bzRagdoll.IsRagdolled = true;
				}
				else if (_health <= Mathf.Epsilon && value > Mathf.Epsilon)
				{
					if (_bzRagdoll != null)
						_bzRagdoll.IsRagdolled = false;
				}
				_health = Mathf.Clamp01(value);
			}
		}
		
		float _impactEndTime;		//Declare a member variables for distributing the impacts over several frames
		Rigidbody _impactTarget;	//Limb to witch an impact was targeted
		Vector3 _impactDirection;

		void Awake()
		{
			_bzRagdoll = GetComponent<IBzRagdoll>();
		}
		
		public void Shot(Ray ray, float force, float distance)
        {
            Debug.DrawRay(ray.origin, ray.origin + ray.direction * distance, Color.red, 2);

            //check if the ray hits a physic collider
            RaycastHit hit;
			if (_bzRagdoll == null)
			{
				if (!Physics.Raycast(ray, out hit, distance))
					return;
			}
			else
			{
				if (!_bzRagdoll.Raycast(ray, out hit, distance))
					return;
			}

			Health -= force;

			Rigidbody hitRigid = hit.rigidbody;
			//check if the raycast target has a rigid body (belongs to the ragdoll)
			if (hitRigid == null || hit.transform.root != transform.root)
				return;
			
			if (!IsDead())
				return;	// exit if still alive

			//set the impact target to whatever the ray hit
			_impactTarget = hitRigid;
			
			//impact direction also according to the ray
			_impactDirection = ray.direction;
			
			//the impact will be reapplied for the next 250ms
			//to make the connected objects follow even though the simulated body joints
			//might stretch
			_impactEndTime = Time.time + 0.25f;
		}

		public bool IsDead()
		{
			return Health <= Mathf.Epsilon;
		}
		
		public bool IsFullHealth()
		{
			return Health >= 1f - Mathf.Epsilon;
		}

		void FixedUpdate()
		{
			//Check if we need to apply an impact
			if (Time.time < _impactEndTime)
			{
				_impactTarget.AddForce(_impactDirection * Time.deltaTime * 80f, ForceMode.VelocityChange);
			}
		}
	}
}

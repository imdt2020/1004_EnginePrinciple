using UnityEngine;

namespace BzKovSoft.RagdollTemplate.Scripts.Charachter
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	public sealed class BzThirdPersonRigid : BzThirdPersonBase
	{
		CapsuleCollider _capsuleCollider;
		Rigidbody _rigidbody;

		protected override void Awake()
		{
			base.Awake();
			_capsuleCollider = GetComponent<CapsuleCollider>();
			_rigidbody = GetComponent<Rigidbody>();

			if (GetComponent<CharacterController>() != null)
				Debug.LogWarning("You do not needed to attach 'CharacterController' to controller with 'Rigidbody'");
		}

		public override void CharacterEnable(bool enable)
		{
			base.CharacterEnable(enable);
			_capsuleCollider.enabled = enable;
			_rigidbody.isKinematic = !enable;
			if (enable)
				_firstAnimatorFrame = true;
		}

		protected override Vector3 PlayerVelocity { get { return _rigidbody.velocity; } }

		protected override void ApplyCapsuleHeight()
		{
			float capsuleY = _animator.GetFloat(_animatorCapsuleY);
			_capsuleCollider.height = capsuleY;
			var c = _capsuleCollider.center;
			c.y = capsuleY / 2f;
			_capsuleCollider.center = c;
		}

#region Ground Check

		/// <summary>
		/// every FixedUpdate _groundChecker resets to false,
		/// and if collision with ground found till next FixedUpdate,
		/// the character on a ground
		/// </summary>
		bool _groundChecker;
		float _jumpStartedTime = -1.0f;
		bool _jumping = false;

		void ProccessOnCollisionOccured(Collision collision)
		{
			// if collision comes from botton, that means
			// that character on the ground
			float charBottom =
				transform.position.y +
				_capsuleCollider.center.y - _capsuleCollider.height / 2 +
				_capsuleCollider.radius * 0.8f;
				
			
			foreach (ContactPoint contact in collision.contacts)
			{
				if (contact.point.y < charBottom && !contact.otherCollider.transform.IsChildOf(transform))
				{
					_groundChecker = true;
					//Debug.DrawRay(contact.point, contact.normal, Color.blue);
					break;
				}
			}
			
			if (!_groundChecker)
			{
				_airVelocity = new Vector3(0, -1.0f, 0);
			}
		}

		void OnCollisionStay(Collision collision)
		{
			ProccessOnCollisionOccured(collision);
		}
		
		void OnCollisionEnter(Collision collision)
		{
			ProccessOnCollisionOccured(collision);
		}

		protected override bool PlayerTouchGound()
		{
			float curTime = Time.time;
			
			if (!_jumping)
			{
				return true;
			}
			
			//Debug.DrawLine(transform.position, transform.position + new Vector3(0, 0.1f, 0), Color.green);
			//Debug.DrawLine(transform.position, transform.position + new Vector3(0, 0, 0.1f), Color.red);
			
			bool grounded = _groundChecker;
			bool justBeginJumping = (curTime - _jumpStartedTime < 0.5f);
			
			if (_jumping && _groundChecker && !justBeginJumping)
			{
				_jumping = false;
			}
			
			_groundChecker = false;
			// if the character is on the ground and
			// half of second was passed, return true
			
			return grounded;// & (_jumpStartedTime + 0.5f < curTime );
		}

#endregion
		protected override void UpdatePlayerPosition(Vector3 deltaPos)
		{
			Vector3 finalVelocity = deltaPos / Time.deltaTime;
			if (!_jumpPressed)
			{
				finalVelocity.y = _rigidbody.velocity.y;
			}
			else
			{
				_jumpStartedTime = Time.time;
				_jumping = true;
			}
			_airVelocity = finalVelocity;		// i need this to correctly detect player velocity in air mode
			_rigidbody.velocity = finalVelocity;
		}
	}
}
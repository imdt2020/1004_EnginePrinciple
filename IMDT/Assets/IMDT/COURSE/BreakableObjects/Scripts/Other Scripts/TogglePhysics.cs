using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSMGameStudio.Behaviours
{
    public class TogglePhysics : MonoBehaviour
    {
        private Rigidbody _rigiBody;
        private Collider _collider;

        private void Awake()
        {
            _rigiBody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }

        /// <summary>
        /// Enable Physics and Collision
        /// </summary>
        public void Enable()
        {
            if (_rigiBody)
                _rigiBody.isKinematic = false;
            if (_collider)
                _collider.enabled = true;
        }

        /// <summary>
        /// Disable Physics and Collision
        /// </summary>
        public void Disable()
        {
            if (_rigiBody)
                _rigiBody.isKinematic = true;
            if (_collider)
                _collider.enabled = false;
        }
    }
}

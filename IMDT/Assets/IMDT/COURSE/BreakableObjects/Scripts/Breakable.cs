using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WSMGameStudio.Settings;

namespace WSMGameStudio.Behaviours
{
    public class Breakable : MonoBehaviour
    {
        public BreakingForce breakingForce;
        public GameObject[] brokenPieces;

        public bool breakOnCollision = false;
        public CollisionSettings collisionSettings;

        public bool RemoveBrokenPiecesFromScene = false;
        public RemoveSettings removeSettings;

        public UnityEvent OnBreak;

        private Collider _collider;
        private Renderer _renderer;
        private AudioSource _breakSFX;
        private Rigidbody _rigidBody;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _renderer = GetComponent<Renderer>();
            _breakSFX = GetComponent<AudioSource>();
            _rigidBody = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Break()
        {
            if (_breakSFX != null)
                _breakSFX.Play();

            _rigidBody.isKinematic = true;

            //Disable collider to avoid collision with spawned broken pieces
            if (_collider != null)
                _collider.enabled = false;

            //Make object dissapear
            _renderer.enabled = false;

            foreach (var piece in brokenPieces)
            {
                GameObject pieceClone = Instantiate(piece, transform.position, transform.rotation);

                if (RemoveBrokenPiecesFromScene)
                {
                    RemoveFromScene removeScript = pieceClone.GetComponent<RemoveFromScene>();
                    removeScript.removeSettings = removeSettings;
                    removeScript.enabled = true;
                    removeScript.InvokeRemoveAction();
                }

                Rigidbody rb = pieceClone.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(breakingForce.power, transform.position, breakingForce.radius, breakingForce.upForce, ForceMode.Impulse);
                }
            }

            if (OnBreak != null)
                OnBreak.Invoke();

            if (_breakSFX != null)
                Invoke("SelfDestroy", _breakSFX.clip.length);
            else
                Invoke("SelfDestroy", 0f);
        }

        private void SelfDestroy()
        {
            Destroy(this.gameObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            if (breakOnCollision)
            {
                switch (collisionSettings.collisionMode)
                {
                    case Enums.CollisionMode.AllObjects:
                        Break();
                        break;
                    case Enums.CollisionMode.CheckAllowedTags:
                        if (collisionSettings.allowedTags.Contains(collision.gameObject.tag))
                            Break();
                        break;
                    case Enums.CollisionMode.CheckIgnoredTags:
                        if (!collisionSettings.ignoredTags.Contains(collision.gameObject.tag))
                            Break();
                        break;
                    case Enums.CollisionMode.CheckAllowedAndIgnoredTags:
                        if (collisionSettings.allowedTags.Contains(collision.gameObject.tag)
                            && !collisionSettings.ignoredTags.Contains(collision.gameObject.tag))
                            Break();
                        break;
                    case Enums.CollisionMode.None:
                        break;
                }
            }
        }
    }
}

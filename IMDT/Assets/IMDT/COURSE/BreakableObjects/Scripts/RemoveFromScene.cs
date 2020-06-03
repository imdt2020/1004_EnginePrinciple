using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WSMGameStudio.Settings;

namespace WSMGameStudio.Behaviours
{
    [RequireComponent(typeof(TogglePhysics))]
    public class RemoveFromScene : MonoBehaviour
    {
        public RemoveSettings removeSettings;

        private TogglePhysics _togglePhysics;

        // Use this for initialization
        void Start()
        {
            InvokeRemoveAction();
        }

        public void InvokeRemoveAction()
        {
            _togglePhysics = GetComponent<TogglePhysics>();
            Invoke("ExecuteRemoveAction", removeSettings.timeInSeconds);
        }

        /// <summary>
        /// Destroy, disable ou disable physics of object (keeps mesh renderer on scene), based on selected Remove Mode
        /// </summary>
        private void ExecuteRemoveAction()
        {
            switch (removeSettings.removeMode)
            {
                case Enums.RemoveMode.Destroy:
                    Destroy(this.gameObject);
                    break;
                case Enums.RemoveMode.Disable:
                    this.gameObject.SetActive(false);
                    break;
                case Enums.RemoveMode.DisablePhysics:
                    _togglePhysics.Disable();
                    break;
                case Enums.RemoveMode.ExecuteOnRemoveEventStack:
                    if (removeSettings.OnRemove != null)
                        removeSettings.OnRemove.Invoke();
                    break;
            }
        }
    }
}

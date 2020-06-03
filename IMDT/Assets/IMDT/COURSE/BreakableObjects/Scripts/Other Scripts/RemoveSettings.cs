using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WSMGameStudio.Settings
{
    [System.Serializable]
    public class RemoveSettings
    {
        public float timeInSeconds;
        public Enums.RemoveMode removeMode;
        public UnityEvent OnRemove;
    } 
}

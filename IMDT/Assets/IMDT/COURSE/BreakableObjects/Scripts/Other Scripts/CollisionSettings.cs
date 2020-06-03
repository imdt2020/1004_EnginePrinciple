using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSMGameStudio.Settings
{
    [System.Serializable]
    public class CollisionSettings
    {
        public Enums.CollisionMode collisionMode;
        public List<string> allowedTags;
        public List<string> ignoredTags;
    } 
}

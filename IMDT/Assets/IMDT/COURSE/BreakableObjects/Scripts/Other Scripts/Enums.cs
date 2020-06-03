using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSMGameStudio.Enums
{
    public enum CollisionMode
    {
        AllObjects,
        CheckAllowedTags,
        CheckIgnoredTags,
        CheckAllowedAndIgnoredTags,
        None
    }

    public enum RemoveMode
    {
        Destroy,
        Disable,
        DisablePhysics,
        ExecuteOnRemoveEventStack
    }
}

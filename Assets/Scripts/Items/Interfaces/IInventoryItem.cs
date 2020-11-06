using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Items
{
    public interface IInventoryItem
    {
        Sprite GetIcon();
        void OnUse(Transform transform);
    }
}

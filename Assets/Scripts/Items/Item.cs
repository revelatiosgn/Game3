using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public abstract Sprite GetIcon();
    public abstract void OnUse();
    public abstract bool IsEquals(Item other);
}

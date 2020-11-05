using UnityEngine;

[System.Serializable]
public class TestEquipmentItem : TestItem
{
    [Range(0f, 1f)] public float condition = 1f;
}

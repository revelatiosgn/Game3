using UnityEngine;

[System.Serializable]
public class TestItemSlot
{
    [SerializeReference] 
    public TestItem item;

    public int count;
}

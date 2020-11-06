using UnityEngine;

public interface TestItem
{
}

[System.Serializable]
public class TestMeleeWeapon : TestItem
{
    public int damage;
}

[System.Serializable]
public class TestRangedWeapon : TestItem
{
    public int range;
}

[System.Serializable]
public class TestPotion : TestItem
{
    public int hp;
}

[System.Serializable]
public class TestArmor : TestItem
{
    public int armor;
}
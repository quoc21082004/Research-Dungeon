using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : ItemSO
{
    public const float HealthPotionCD = 5f;
    public const float ManaPotionCD = 5f;
    public const float StatPotionCD = 0f;
    public ConsumableType consumableType;
    public SpellBookType type;
    public static float GetConsumtableTypeCD(ConsumableType type)
    {
        switch(type)
        {
            case ConsumableType.HealthPotion:
                return HealthPotionCD;
            case ConsumableType.ManaPotion:
                return ManaPotionCD;
            case ConsumableType.StatPotion:
                return StatPotionCD;
            default:
                return -1;
        }
    }
}
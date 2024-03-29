using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Shop Item", menuName = "Shop/Potion")]
public class Potion : Consumable
{
    public const float HealthPotionCD = 5f;
    public const float ManaPotionCD = 5f;
    public ConsumableType consumableType;
    public static float GetConsumtableTypeCD(ConsumableType type)
    {
        switch (type)
        {
            case ConsumableType.HealthPotion:
                return HealthPotionCD;
            case ConsumableType.ManaPotion:
                return ManaPotionCD;
            default:
                return -1;
        }
    }
    public override void Use()
    {
        switch(consumableType)
        {
            case ConsumableType.HealthPotion:
                float recoverHP = (value * PartyController.player.GetComponent<Player>().maxhealth) / 100;
                PartyController.player.gameObject.GetComponent<PlayerHurt>().PlayerRecoverHP(recoverHP);
                AssetManager.instance.assetData.SpawnRecoverEffect(consumableType, PartyController.player.transform.position, PartyController.player.transform);
                break;
            case ConsumableType.ManaPotion:
                float recoverMP = (value * PartyController.player.GetComponent<Player>().maxmana) / 100;
                PartyController.player.gameObject.GetComponent<PlayerHurt>().PlayerRecoverMP(recoverMP);
                AssetManager.instance.assetData.SpawnRecoverEffect(consumableType, PartyController.player.transform.position, PartyController.player.transform);
                break;
            default:
                break;
        }
    }
}

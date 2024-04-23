using UnityEngine;

public static class LayerMaskHelper
{
    public static LayerMask layerMaskEnemy = LayerMask.GetMask("Enemy");

    public static LayerMask layerMaskLoot = LayerMask.GetMask("Loot");

    public static LayerMask layerMaskPlayer = LayerMask.GetMask("PlayerCTL");
}
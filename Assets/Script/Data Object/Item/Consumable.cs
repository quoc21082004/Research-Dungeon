using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : ItemSO
{
    public int value;
    public override void Use()
    {
        base.Use();
    }
}
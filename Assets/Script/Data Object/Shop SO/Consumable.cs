using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : ItemSO
{
    public int value;
    public abstract void Use();
}
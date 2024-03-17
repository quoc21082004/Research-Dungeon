using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemReward
{
    [SerializeField] public ItemSO item;
    [SerializeField, Tooltip("Value of Reweward")] public int value;
    [SerializeField, Tooltip("Random Value")] public bool isRandom;
    [SerializeField] public Vector2 valueRandom;

}

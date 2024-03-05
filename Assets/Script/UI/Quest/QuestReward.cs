using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemReward
{
    [SerializeField] private ItemSO item;
    [SerializeField, Tooltip("Value of Reweward")] private int value;
    [SerializeField, Tooltip("Random Value")] private bool isRandom;
    [SerializeField] private Vector2 valueRandom;

    public int GetValueRandom() => isRandom ? (int)Random.Range(valueRandom.x, valueRandom.y) : value;

}

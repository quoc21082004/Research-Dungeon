using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemReward
{
    [SerializeField] public int CoinReward;
    [SerializeField] public ItemSO item;
    [SerializeField, Tooltip("Value of Reweward")] public int value;
    [SerializeField, Tooltip("Random Value")] public bool isRandom;
    [SerializeField] public Vector2 valueRandom;

    public int GetValueRandom() => isRandom ? (int)Random.Range(valueRandom.x, valueRandom.y) : value;
    public int GetCoinReward() => isRandom ? (int)Random.Range(valueRandom.x, valueRandom.y) * CoinReward : CoinReward;

}

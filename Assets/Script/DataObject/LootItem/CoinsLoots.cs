using UnityEngine;
public class CoinsLoots : LootItem
{
    [SerializeField] int rand;
    private void Start() => rand = Random.Range(2, 5) * PlayerPrefs.GetInt("doubleDrop") * PlayerPrefs.GetInt("extraCoins");
    protected override void PickUp()
    {
        PartyController.IncreaseCoin((int)rand);
        //PartyController.inventoryG.IncreaseGold((int)rand);
        base.PickUp();
    }

}



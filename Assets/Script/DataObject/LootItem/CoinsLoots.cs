using UnityEngine;
public class CoinsLoots : LootItem
{
    [SerializeField] private GameObject coinprefab;
    [SerializeField][Range(10, 15)] private int rand;
    private void Start() => rand = Random.Range(rand, Mathf.CeilToInt(rand * 1.5f));
    protected override void PickUp()
    {
        PartyController.inventoryG.IncreaseCoin((int)rand);
        NoticeManager.instance.CreateNoticeLeftItem(coinprefab.GetComponent<SpriteRenderer>().sprite, $"Gold x{rand}");
        NoticeManager.instance.EnableNoticeLeftItem();
        base.PickUp();
    }

}



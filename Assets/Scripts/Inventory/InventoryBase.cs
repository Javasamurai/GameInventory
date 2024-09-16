using UnityEngine;

public abstract class InventoryBase
{
    [SerializeField]
    protected GameObject inventoryItemPrefab;
    public ItemDatabase itemDatabase;

    abstract public void SpawnItems(Transform content, GameObject inventoryItem, GameObject inventoryPanel, ItemType itemType = ItemType.None);

    public void BuyItem(Item item)
    {
        // Show popup
    }

    public void SellItem(Item item)
    {
        // Show popup
    }

    protected InventoryBase(ItemDatabase itemDatabase)
    {
        this.itemDatabase = itemDatabase;
    }
}
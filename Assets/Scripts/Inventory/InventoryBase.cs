using UnityEngine;

public abstract class InventoryBase
{
    public ItemDatabase itemDatabase;

    abstract public void SpawnItems(Transform content, GameObject inventoryItem, GameObject inventoryPanel, ItemType itemType = ItemType.None);

    public void BuyItem(Item item, int maxQuantity)
    {
        // Show popup
        PopupManager.Instance.ShowBuySellPopup(item, maxQuantity, INVENTORY_TYPE.SHOP);
    }

    public void SellItem(Item item, int maxQuantity)
    {
        // Show popup
        PopupManager.Instance.ShowBuySellPopup(item, maxQuantity, INVENTORY_TYPE.PLAYER);
    }

    protected InventoryBase(ItemDatabase itemDatabase)
    {
        this.itemDatabase = itemDatabase;
    }
}
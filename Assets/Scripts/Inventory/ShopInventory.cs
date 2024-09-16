using UnityEngine;

class ShopInventory : InventoryBase
{
    private ShopView inventoryView;
    public ShopInventory(ItemDatabase itemDatabase, ShopView shopView) : base(itemDatabase)
    {
        this.inventoryView = shopView;
        this.inventoryView.SetController(this);
        this.inventoryView.Init();
    }

    public override void SpawnItems(Transform content, GameObject inventoryItem, GameObject inventoryPanel, ItemType itemType = ItemType.None)
    {
        if (content.childCount > 0)
        {
            foreach (Transform child in content)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        foreach (Item item in itemDatabase.items)
        {
            if (itemType != ItemType.None && item.itemType != itemType) continue;
            GameObject itemObject = GameObject.Instantiate(inventoryItem, content);
            itemObject.GetComponent<InventoryItem>().Init(item, item.quantity, INVENTORY_TYPE.SHOP, this);
        }
    }
}
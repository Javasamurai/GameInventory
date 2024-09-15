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
                Destroy(child.gameObject);
            }
        }
        foreach (Item item in itemDatabase.items)
        {
            if (itemType != ItemType.None && item.itemType != itemType) continue;
            GameObject itemObject = Instantiate(inventoryItem, content);
            itemObject.GetComponent<InventoryItem>().Init(item, InventoryItem.INVENTORY_TYPE.SHOP);
        }
    }

    protected override void OnClickItem(Item item)
    {
        Debug.Log("Item clicked: " + item.name);
    }
}
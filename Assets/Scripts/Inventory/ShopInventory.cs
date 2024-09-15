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

    public override void SpawnItems(Transform content, GameObject inventoryItem, GameObject inventoryPanel)
    {
        foreach (Item item in itemDatabase.items)
        {
            GameObject itemObject = Instantiate(inventoryItem, content);
            itemObject.GetComponent<InventoryItem>().Init(item, InventoryItem.INVENTORY_TYPE.SHOP);
        }
    }

    protected override void OnClickItem(Item item)
    {
        Debug.Log("Item clicked: " + item.name);
    }
}
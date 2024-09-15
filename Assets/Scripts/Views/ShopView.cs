using UnityEngine;

class ShopView : MonoBehaviour
{
    private ShopInventory shopInventory;

    [SerializeField]
    private InventoryItem inventoryItem;
    [SerializeField]
    private Transform content;
    [SerializeField]

    private TabView tabView;

    [SerializeField]
    private ItemType defaultItemType = ItemType.Weapons;

    public void SetController(ShopInventory shopInventory)
    {
        this.shopInventory = shopInventory;
    }

    public void Init()
    {
        tabView.Init(this, defaultItemType);
        shopInventory.SpawnItems(content, inventoryItem.gameObject, gameObject, defaultItemType);
    }

    public void RefreshItems(ItemType itemType)
    {
        shopInventory.SpawnItems(content, inventoryItem.gameObject, gameObject, itemType);
    }
}
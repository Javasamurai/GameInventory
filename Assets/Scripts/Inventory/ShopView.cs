using UnityEngine;

class ShopView : MonoBehaviour
{
    private ShopInventory shopInventory;

    [SerializeField]
    private InventoryItem inventoryItem;
    [SerializeField]
    private Transform content;
    public void SetController(ShopInventory shopInventory)
    {
        this.shopInventory = shopInventory;
    }

    public void Init()
    {
        shopInventory.SpawnItems(content, inventoryItem.gameObject, gameObject);
    }
}
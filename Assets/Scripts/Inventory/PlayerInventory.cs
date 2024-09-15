using UnityEngine;

class PlayerInventory : InventoryBase
{
    private PlayerInventoryView inventoryView;
    public PlayerInventory(ItemDatabase itemDatabase, PlayerInventoryView playerInventoryView) : base(itemDatabase)
    {
        this.inventoryView = playerInventoryView;
    }

    public override void SpawnItems(Transform content, GameObject inventoryItem, GameObject inventoryPanel)
    {
        
    }
}
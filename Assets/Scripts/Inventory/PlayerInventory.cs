using UnityEngine;

public class PlayerInventory : InventoryBase
{
    private PlayerInventoryView inventoryView;
    public PlayerInventory(ItemDatabase itemDatabase, PlayerInventoryView playerInventoryView) : base(itemDatabase)
    {
        this.inventoryView = playerInventoryView;
        this.inventoryView.SetController(this);
        this.inventoryView.Init();
    }

    public override void SpawnItems(Transform content, GameObject inventoryItem, GameObject inventoryPanel, ItemType itemType = ItemType.None)
    {
        
    }
}
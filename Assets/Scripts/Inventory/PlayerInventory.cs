using System.Linq;
using UnityEngine;

public class PlayerInventory : InventoryBase
{
    public int walletWeight {
        get {
            SavedItem[] savedItems = PlayerWallet.OwnedItems;
            int weight = 0;
            foreach (SavedItem item in savedItems)
            {
                Item itemData = itemDatabase.items.FirstOrDefault(i => i.name == item.itemName);
                if (itemData != null)
                {
                    weight += itemData.weight * item.quantity;
                }
            }
            return weight;
        }
    } 

    private PlayerInventoryView inventoryView;
    public PlayerInventory(ItemDatabase itemDatabase, PlayerInventoryView playerInventoryView) : base(itemDatabase)
    {
        this.inventoryView = playerInventoryView;
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

        SavedItem[] ownedItemsArray = PlayerWallet.OwnedItems;

        for (int i = 0; i < ownedItemsArray.Length; i++)
        {
            SavedItem currentItem = ownedItemsArray[i];
            Item item = itemDatabase.items.FirstOrDefault(i => i.name == currentItem.itemName);

            if (item != null)
            {
                GameObject itemObject = GameObject.Instantiate(inventoryItem, content);
                InventoryItem inventoryItemComponent = itemObject.GetComponent<InventoryItem>();
                inventoryItemComponent.Init(item, currentItem.quantity, INVENTORY_TYPE.PLAYER, this);
            }
        }
    }

    public void GatherItems()
    {
        // Collect random items
        int randomItemIndex = Random.Range(0, itemDatabase.items.Length);
        Item item = itemDatabase.items[randomItemIndex];
        
        if (!PlayerWallet.Instance.CanHold(item))
        {
            GatherItems();
            return;
        }
        PlayerWallet.Instance.AddItem(item);
    }
}
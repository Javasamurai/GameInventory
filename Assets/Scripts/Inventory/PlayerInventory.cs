using System.Linq;
using UnityEngine;

public class PlayerInventory : InventoryBase
{
    private const string OWNEDITEMSKEY = "ownedItems";
    private const string COINSKEY = "Coins";
    public static int Coins => PlayerPrefs.GetInt(COINSKEY, 0);

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
        foreach (Item item in itemDatabase.items)
        {
            SavedItem currentItem = ownedItemsArray.FirstOrDefault(i => i.itemName == item.name);
            if (ownedItemsArray.Length > 0)
            {
                if (!ownedItemsArray.Any(i => i.itemName == item.name))
                {
                    continue;
                }
            }
            else
            {
                break;
            }
            GameObject itemObject = GameObject.Instantiate(inventoryItem, content);
            InventoryItem inventoryItemComponent = itemObject.GetComponent<InventoryItem>();
            inventoryItemComponent.Init(item, currentItem.quantity, INVENTORY_TYPE.PLAYER, this);
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
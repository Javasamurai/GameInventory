using System.Linq;
using UnityEngine;

public class PlayerInventory : InventoryBase
{
    private const string OWNEDITEMSKEY = "ownedItems";
    private const string COINSKEY = "Coins";
    public static int Coins => PlayerPrefs.GetInt(COINSKEY, 0);
    public static string[] OwnedItems => PlayerPrefs.GetString(OWNEDITEMSKEY, "").Split(',');

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
                Destroy(child.gameObject);
            }
        }

        string[] ownedItemsArray = PlayerWallet.OwnedItems;
        foreach (Item item in itemDatabase.items)
        {
            if (!ownedItemsArray.Contains(item.name)) continue;
            GameObject itemObject = Instantiate(inventoryItem, content);
            InventoryItem inventoryItemComponent = itemObject.GetComponent<InventoryItem>();
            inventoryItemComponent.Init(item, InventoryItem.INVENTORY_TYPE.PLAYER);
            if (ownedItemsArray.Length > 0)
            {
                foreach (string ownedItem in ownedItemsArray)
                {
                    if (ownedItem == item.name)
                    {
                        inventoryItemComponent.gameObject.SetActive(true);
                        break;
                    }
                }
            }
        }
    }

    public void GatherItems()
    {
        // Collect random items
        int randomItemIndex = Random.Range(0, itemDatabase.items.Length);
        Item item = itemDatabase.items[randomItemIndex];
        // Keep collecting until we get an item we don't already own
        if (PlayerWallet.OwnedItems.Contains(item.name))
        {
            GatherItems();
            return;
        }
        PlayerWallet.Instance.AddItem(item);
        EventService.Instance.OnItemPurchased.Invoke(item);
    }
}
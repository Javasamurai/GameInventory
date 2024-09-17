using System;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerWallet
{
    private const string OWNEDITEMSKEY = "ownedItems";
    private const string COINSKEY = "Coins";
    public static float Coins => PlayerPrefs.GetFloat(COINSKEY, 0);

    public int currentWalletWeight = 0;
    public static SavedItem[] OwnedItems
    {
        get
        {
            string ownedItems = PlayerPrefs.GetString(OWNEDITEMSKEY, "");
            SavedItem[] savedItems = JsonConvert.DeserializeObject<SavedItem[]>(ownedItems);
            if (savedItems == null) return new SavedItem[0];
            return savedItems;
        }
    }

    public static PlayerWallet Instance { get; private set; }

    private WalletConfig walletConfig;

    public PlayerWallet(WalletConfig walletConfig)
    {
        this.walletConfig = walletConfig;
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddCoins(int amount)
    {
        PlayerPrefs.SetFloat(COINSKEY, Coins + amount);
        PlayerPrefs.Save();
    }

    public void RemoveCoins(float amount)
    {
        PlayerPrefs.SetFloat(COINSKEY, Coins - amount);
        PlayerPrefs.Save();
    }

    public void AddItem(Item item, int quantity = 1)
    {
        SavedItem[] savedItems = OwnedItems;
        if (CanHold(item))
        {
            if (savedItems == null || savedItems.Length == 0)
            {
                savedItems = new SavedItem[] { new SavedItem { itemName = item.name, quantity = quantity } };
            }
            else if (savedItems.Any(i => i.itemName == item.name))
            {
                savedItems.First(i => i.itemName == item.name).quantity += quantity;
            }
            else
            {
                SavedItem[] newItems = new SavedItem[savedItems.Length + 1];
                savedItems.CopyTo(newItems, 0);
                newItems[savedItems.Length] = new SavedItem { itemName = item.name, quantity = quantity };
                savedItems = newItems;
            }
            // Convert the array to a JSON string and save it to the Persistent Data Path
            string json = JsonConvert.SerializeObject(savedItems);
            PlayerPrefs.SetString(OWNEDITEMSKEY, json);
            currentWalletWeight += item.weight * quantity;
        }
    }

    public void RemoveItem(Item item, int quantity)
    {
        string ownedItems = PlayerPrefs.GetString(OWNEDITEMSKEY, "");
        SavedItem[] savedItems = JsonConvert.DeserializeObject<SavedItem[]>(ownedItems);
        if (savedItems.Any(i => i.itemName == item.name))
        {
            SavedItem savedItem = savedItems.First(i => i.itemName == item.name);
            if (item.quantity > 1)
            {
                item.quantity-= quantity;
            }
            else
            {
                savedItems = savedItems.Where(i => i.itemName != item.name).ToArray();
            }
            PlayerPrefs.SetString(OWNEDITEMSKEY, JsonConvert.SerializeObject(savedItems));
            currentWalletWeight -= item.weight * quantity;
        }
    }

    public bool CanAfford(Item item, int quantity = 1)
    {
        return Coins >= item.buyingPrice * quantity;
    }
    public bool CanHold(Item item, int quantity = 1)
    {
        return currentWalletWeight + (item.weight * quantity) <= walletConfig.maxWeight;
    }

    public void PurchaseItem(Item item, int cost)
    {
        if (!CanAfford(item, 1)) return;
        if (CanHold(item)) return;
        RemoveCoins(cost);
        AddItem(item);
    }

    public static bool OwnsItem(string itemName)
    {
        return OwnedItems.FirstOrDefault(i => i.itemName == itemName) != null;
    }
}

[Serializable]
public class SavedItem
{
    public string itemName;
    public int quantity;
}
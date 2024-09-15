using System.Linq;
using UnityEngine;

public class PlayerWallet
{
    private const string OWNEDITEMSKEY = "ownedItems";
    private const string COINSKEY = "Coins";
    public static int Coins => PlayerPrefs.GetInt(COINSKEY, 0);
    public static string[] OwnedItems => PlayerPrefs.GetString(OWNEDITEMSKEY, "")?.Split(',');

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
        PlayerPrefs.SetInt(COINSKEY, Coins + amount);
        PlayerPrefs.Save();
    }

    public void RemoveCoins(int amount)
    {
        PlayerPrefs.SetInt(COINSKEY, Coins - amount);
        PlayerPrefs.Save();
    }

    public void AddItem(Item item)
    {
        string ownedItems = PlayerPrefs.GetString(OWNEDITEMSKEY, "");
        if (!OwnsItem(item.name))
        {
            ownedItems += item.name + ",";
            PlayerPrefs.SetString(OWNEDITEMSKEY, ownedItems);
            PlayerPrefs.Save();
        }
    }

    public void RemoveItem(string itemName)
    {
        string ownedItems = PlayerPrefs.GetString(OWNEDITEMSKEY, "");
        ownedItems = ownedItems.Replace(itemName + ",", "");
        PlayerPrefs.SetString(OWNEDITEMSKEY, ownedItems);
        PlayerPrefs.Save();
    }

    public bool CanAfford(int cost)
    {
        return Coins >= cost;
    }
    public bool CanHold(Item item)
    {
        return item.weight <= walletConfig.maxWeight;
    }

    public void PurchaseItem(Item item, int cost)
    {
        if (!CanAfford(cost)) return;
        if (CanHold(item)) return;
        RemoveCoins(cost);
        AddItem(item);
    }

    public static bool OwnsItem(string itemName)
    {
        return OwnedItems.Contains(itemName);
    }
}
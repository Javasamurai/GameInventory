using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private WalletConfig walletConfig;

    [SerializeField] private ShopView shopView;
    [SerializeField] private PlayerInventoryView playerInventoryView;

    ShopInventory shopInventory;
    PlayerInventory playerInventory;
    PlayerWallet playerWallet;

    private void Awake()
    {
        playerWallet = new PlayerWallet(walletConfig);
        shopInventory = new ShopInventory(itemDatabase, shopView);
        playerInventory = new PlayerInventory(itemDatabase, playerInventoryView);
    }
}
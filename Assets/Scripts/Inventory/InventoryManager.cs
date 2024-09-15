using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private ItemDatabase itemDatabase;

    [SerializeField] private ShopView shopView;
    [SerializeField] private PlayerInventoryView playerInventoryView;

    ShopInventory shopInventory;
    PlayerInventory playerInventory;

    private void Awake()
    {
        shopInventory = new ShopInventory(itemDatabase, shopView);
        playerInventory = new PlayerInventory(itemDatabase, playerInventoryView);
    }
}
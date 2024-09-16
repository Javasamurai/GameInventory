using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryView : MonoBehaviour
{
    [SerializeField]
    private Transform content;
    [SerializeField]
    private InventoryItem inventoryItem;
    [SerializeField]
    private Button gatherButton;
    private PlayerInventory playerInventory;
    public void SetController(PlayerInventory playerInventory)
    {
        this.playerInventory = playerInventory;
    }

    public void Init()
    {
        EventService.Instance.OnItemPurchased.AddListener(OnItemPurchased);
        gatherButton.onClick.AddListener(() => GatherItems());
        playerInventory.SpawnItems(content, inventoryItem.gameObject, gameObject);
    }

    private void GatherItems()
    {
        playerInventory.GatherItems();
        Item dummyItem = new Item { name = "Dummy", weight = 1 };

        if (PlayerWallet.Instance.CanHold(dummyItem))
        {
            gatherButton.interactable = true;
        }
        else
        {
            gatherButton.interactable = false;
        }
        playerInventory.SpawnItems(content, inventoryItem.gameObject, gameObject);
    }

    public void OnItemPurchased(Item item)
    {
        PlayerWallet.Instance.AddItem(item);
        playerInventory.SpawnItems(content, inventoryItem.gameObject, gameObject);
    }

    private void OnDestroy()
    {
        EventService.Instance.OnItemPurchased.RemoveListener(OnItemPurchased);
        gatherButton.onClick.RemoveAllListeners();
    }
}
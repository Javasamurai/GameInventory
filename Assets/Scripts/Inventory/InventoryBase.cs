using UnityEngine;

public abstract class InventoryBase : MonoBehaviour
{
    [SerializeField]
    protected GameObject inventoryPanel;
    [SerializeField]
    protected GameObject inventoryItemPrefab;
    protected ItemDatabase itemDatabase;

    abstract public void SpawnItems(Transform content, GameObject inventoryItem, GameObject inventoryPanel);

    protected InventoryBase(ItemDatabase itemDatabase)
    {
        this.itemDatabase = itemDatabase;
    }

    protected virtual void OnClickItem(Item item)
    {
        Debug.Log("Item clicked: " + item.name);
    }

    protected virtual void OnPointerEnterItem(Item item)
    {
        Debug.Log("Pointer Enter Item: " + item.name);
    }

    protected virtual void OnPointerExitItem(Item item)
    {
        Debug.Log("Pointer Exit Item: " + item.name);
    }
}
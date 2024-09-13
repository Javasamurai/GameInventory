using UnityEngine;

public abstract class InventoryBase : MonoBehaviour
{
    [SerializeField]
    protected GameObject inventoryPanel;
    [SerializeField]
    protected GameObject inventoryItemPrefab;
    [SerializeField]
    protected ItemDatabase itemDatabase;
    abstract public void AddItem(Item item);
    abstract public void RemoveItem(Item item);

    private void Awake() {
        InstantiateItems();
    }

    virtual public void InstantiateItems()
    {
        foreach (Item item in itemDatabase.items)
        {
            GameObject itemObject = Instantiate(inventoryItemPrefab, transform);
            itemObject.transform.SetParent(inventoryPanel.transform);
            itemObject.GetComponent<InventoryItem>().SetItem(item);
        }
    }
}
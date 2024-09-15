using System;
using UnityEngine;

public abstract class InventoryBase : MonoBehaviour
{
    [SerializeField]
    protected GameObject inventoryItemPrefab;
    public ItemDatabase itemDatabase;

    abstract public void SpawnItems(Transform content, GameObject inventoryItem, GameObject inventoryPanel, ItemType itemType = ItemType.None);

    public void BuyItem(Item item)
    {
        
    }

    public void SellItem(Item item)
    {

    }

    protected InventoryBase(ItemDatabase itemDatabase)
    {
        this.itemDatabase = itemDatabase;
    }
}
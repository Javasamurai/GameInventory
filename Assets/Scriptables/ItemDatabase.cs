using System;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Scriptables/ItemDatabase", order = 1)]
public class ItemDatabase : ScriptableObject
{
    public Item[] items;
    public InventoryItemPreview itemPreviewPrefab;
}


[Serializable]
public class Item
{
    public ItemType itemType;
    public string name;
    public Sprite icon;
    public string description;
    public int buyingPrice;
    public int sellingPrice;
    public int weight;
    public Rarity rarity;
    public int quantity;
}

public enum Rarity
{
    VeryCommon,
    Common,
    Rare,
    Epic,
    Legendary
}

public enum ItemType
{
    None,
    Material,
    Weapons,
    Consumable,
    Treasure
}
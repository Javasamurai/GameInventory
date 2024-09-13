using System;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Scriptables/ItemDatabase", order = 1)]
public class ItemDatabase : ScriptableObject
{
    public Item[] items;
}


[Serializable]
public class Item
{
    public ItemType itemType;
    public string name;
    public Sprite icon;
    public string description;
    public int buyingPrice;
    public int value;
    public int damage;
    public int armor;
    public Rarity rarity;
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
    Material,
    Weapons,
    Consumable,
    Treasure
}
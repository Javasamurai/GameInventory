using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySpawner : MonoBehaviour
{
    [SerializeField]
    public ItemDatabase itemDatabase;


    [SerializeField]
    private InventoryItem itemPrefab;

    [SerializeField]
    private Transform itemParent;

    void Start()
    {
        SpawnItems();
    }

    private void SpawnItems()
    {
        foreach (Item item in itemDatabase.items)
        {
            Debug.Log("Spawning " + item.name);
            Instantiate(itemPrefab, itemParent);
        }
    }
}

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabView : MonoBehaviour
{
    [SerializeField] private GameObject tabButtonPrefab;
    [SerializeField] private ItemType defaultItemType = ItemType.Weapons;

    private List<TabButton> tabButtons = new List<TabButton>();
    private ShopView shopView;
    private ToggleGroup toggleGroup;

    private void Awake()
    {
        toggleGroup = GetComponent<ToggleGroup>();
        foreach (ItemType itemType in Enum.GetValues(typeof(ItemType))) {
            if (itemType == ItemType.None) continue;
            GameObject tabButton = Instantiate(tabButtonPrefab, transform);
            tabButton.GetComponentInChildren<TextMeshProUGUI>().text = itemType.ToString();
            Toggle toggle = tabButton.GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(isOn => {
                var currentItemType = itemType; 
                if (isOn) {
                    // Render items based on itemType
                    shopView.RefreshItems(currentItemType);
                }
            });
            toggle.group = toggleGroup;
            tabButtons.Add(new TabButton { itemType = itemType, tabButton = toggle});
            tabButton.SetActive(true);
        }
    }

    public void SetTab(ItemType itemType) {
        foreach (TabButton tabButton in tabButtons) {
            tabButton.tabButton.isOn = tabButton.itemType == itemType;
        }
    }

    internal void Init(ShopView shopView, ItemType defaultItemType)
    {
        this.shopView = shopView;
        SetTab(defaultItemType);
    }
}

struct TabButton {
    public ItemType itemType;
    public Toggle tabButton;
}
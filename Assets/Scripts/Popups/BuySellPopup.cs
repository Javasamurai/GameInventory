using System;
using UnityEngine;
using UnityEngine.UI;

public class BuySellPopup : MonoBehaviour {
    [SerializeField] private TMPro.TextMeshProUGUI infoText;
    [SerializeField] private Button buySellButton;
    [SerializeField] private Button closeButton;

    private Item item;
    private INVENTORY_TYPE inventoryType;

    public void Init(Item item, int quantity, INVENTORY_TYPE inventoryType) {
        this.item = item;
        this.inventoryType = inventoryType;
        string action = inventoryType == INVENTORY_TYPE.SHOP ? "Buy" : "Sell";
        infoText.text = "Do you want to" + action + " " + quantity + " " + item.name + " for " + item.buyingPrice * quantity + " coins?";
        gameObject.SetActive(true);
        buySellButton.onClick.AddListener(OnBuySellButtonClicked);
        closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    private void OnBuySellButtonClicked()
    {
        if (inventoryType == INVENTORY_TYPE.SHOP)
        {
            if (PlayerWallet.Coins >= item.buyingPrice) {
                PlayerWallet.Instance.RemoveCoins(item.buyingPrice);
                PlayerWallet.Instance.AddItem(item, 1);
            }
            EventService.Instance.OnItemPurchased.Invoke(item);
        }
        else
        {
            PlayerWallet.Instance.RemoveItem(item, 1);
            PlayerWallet.Instance.AddCoins(item.sellingPrice);
            EventService.Instance.OnItemSold.Invoke(item);
        }
        gameObject.SetActive(false);
    }


    private void OnCloseButtonClicked()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        buySellButton.onClick.RemoveListener(OnBuySellButtonClicked);
        closeButton.onClick.RemoveListener(OnCloseButtonClicked);
    }
}
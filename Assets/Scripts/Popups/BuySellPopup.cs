using System;
using UnityEngine;
using UnityEngine.UI;

public class BuySellPopup : MonoBehaviour {
    [SerializeField] private TMPro.TextMeshProUGUI titleText;
    
    [SerializeField] private TMPro.TextMeshProUGUI infoText;
    [SerializeField] private TMPro.TextMeshProUGUI purchaseText;
    [SerializeField] private Button buySellButton;
    [SerializeField] private Button closeButton;
    [SerializeField] Slider slider;

    private Item item;
    private INVENTORY_TYPE inventoryType;

    public void Init(Item item, int maxQuantity, INVENTORY_TYPE inventoryType)
    {
        this.item = item;
        this.inventoryType = inventoryType;
        
        string action = inventoryType == INVENTORY_TYPE.SHOP ? "buy" : "sell";
        purchaseText.text = "Do you want to " + action + " " + item.name + "?";
        titleText.text = inventoryType == INVENTORY_TYPE.SHOP ? "Buy item" : "Sell item";
        gameObject.SetActive(true);

        if (item.buyingPrice * maxQuantity > PlayerWallet.Coins && inventoryType == INVENTORY_TYPE.SHOP)
        {
            buySellButton.interactable = false;
        }
        else if (PlayerWallet.Instance.CanHold(item) && inventoryType == INVENTORY_TYPE.PLAYER)
        {
            buySellButton.interactable = true;
        }

        buySellButton.onClick.AddListener(OnBuySellButtonClicked);
        closeButton.onClick.AddListener(OnCloseButtonClicked);
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        slider.minValue = 1;
        slider.maxValue = maxQuantity;
        OnSliderValueChanged(slider.value);
    }

    private void OnSliderValueChanged(float value)
    {
        int quantity = (int)value;
        string action = inventoryType == INVENTORY_TYPE.SHOP ? "buy" : "sell";

        bool canHold = PlayerWallet.Instance.CanHold(item, quantity);
        bool canBuy = PlayerWallet.Instance.CanAfford(item, quantity);

        infoText.text = "Do you want to " + action + " " + quantity + " " + item.name + " for " + item.buyingPrice * quantity + " coins?";
        buySellButton.interactable = canHold && canBuy && inventoryType == INVENTORY_TYPE.PLAYER;
        buySellButton.interactable = item.buyingPrice * quantity <= PlayerWallet.Coins && inventoryType == INVENTORY_TYPE.SHOP;

        if (inventoryType == INVENTORY_TYPE.SHOP)
        {
            buySellButton.interactable = value > 0 && item.buyingPrice * quantity <= PlayerWallet.Coins;

            if (!canHold)
            {
                infoText.text = "You can't hold that, you're too weak!";
            }
            else if (!canBuy)
            {
                infoText.text = "You can't afford that, you peasant!";
            }
            else
            {
                infoText.text = "Do you want to " + action + " " + quantity + " " + item.name + " for " + item.buyingPrice * quantity + " coins?";
            }
        }
        else
        {
            buySellButton.interactable = value > 0;        
            infoText.text = "Do you want to " + action + " " + quantity + " " + item.name + " for " + item.buyingPrice * quantity + " coins?";
        }
    }

    private void OnBuySellButtonClicked()
    {
        if (inventoryType == INVENTORY_TYPE.SHOP)
        {
            if (PlayerWallet.Coins >= item.buyingPrice * slider.value)
            {
                PlayerWallet.Instance.RemoveCoins(item.buyingPrice * slider.value);
                PlayerWallet.Instance.AddItem(item, (int) slider.value);
            }
            EventService.Instance.OnItemPurchased.Invoke(item);
        }
        else
        {
            PlayerWallet.Instance.RemoveItem(item, (int) slider.value);
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
        slider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}
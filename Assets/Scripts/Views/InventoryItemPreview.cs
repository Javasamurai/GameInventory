using UnityEngine;
using UnityEngine.UI;

public class InventoryItemPreview : MonoBehaviour
{
    private Item item;

    [SerializeField]
    private Image icon;
    [SerializeField]
    private TMPro.TextMeshProUGUI itemNameText;
    [SerializeField]
    private TMPro.TextMeshProUGUI itemDescriptionText;
    [SerializeField]
    private TMPro.TextMeshProUGUI itemPriceText;

    [SerializeField]
    private TMPro.TextMeshProUGUI buySellText;

    public void Init(Item item, INVENTORY_TYPE inventoryType)
    {
        this.item = item;
        itemNameText.text = item.name;
        itemDescriptionText.text = item.description;
        itemPriceText.text = item.buyingPrice.ToString();
        icon.sprite = item.icon;
        if (inventoryType == INVENTORY_TYPE.SHOP)
        {
            buySellText.text = "Click to buy item";
        }
        else
        {
            buySellText.text = "Click to Sell item";
        }
    }
}
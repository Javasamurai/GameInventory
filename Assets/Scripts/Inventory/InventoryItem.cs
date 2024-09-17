using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public enum INVENTORY_TYPE
{
    SHOP,
    PLAYER
}

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private InventoryItemPreview inventoryItemPreview;

    private Item item;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TMPro.TextMeshProUGUI itemQuantityText;

    private InventoryBase inventoryBase;

    

    private INVENTORY_TYPE inventoryType; 

    public void Init(Item item, int quantity, INVENTORY_TYPE inventoryType, InventoryBase inventoryBase)
    {
        this.item = item;
        icon.sprite = item.icon;
        itemQuantityText.text = quantity.ToString();
        this.inventoryType = inventoryType;
        this.inventoryBase = inventoryBase;
    }

    public Item GetItem()
    {
        return item;
    }

    private void Update()
    {
        if (inventoryItemPreview != null)
        {
            inventoryItemPreview.transform.position = Input.mousePosition - new Vector3(0, 200, 0);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inventoryItemPreview != null)
        {
            Destroy(inventoryItemPreview.gameObject);
        }
        inventoryItemPreview = Instantiate(inventoryBase. itemDatabase.itemPreviewPrefab, this.transform.parent.parent);
        inventoryItemPreview.Init(item, inventoryType);
        inventoryItemPreview.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (inventoryItemPreview != null)
        {
            Destroy(inventoryItemPreview.gameObject);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventoryType == INVENTORY_TYPE.SHOP)
        {
            inventoryBase.BuyItem(item);
        }
        else
        {
            inventoryBase.SellItem(item);
        }
    }
}
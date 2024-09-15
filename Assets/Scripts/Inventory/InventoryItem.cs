using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Item item;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TMPro.TextMeshProUGUI itemQuantityText;

    public enum INVENTORY_TYPE
    {
        SHOP,
        PLAYER
    }

    private INVENTORY_TYPE inventoryType; 

    public void Init(Item item, INVENTORY_TYPE inventoryType)
    {
        this.item = item;
        icon.sprite = item.icon;
        itemQuantityText.text = item.name;
        this.inventoryType = inventoryType;
    }

    public Item GetItem()
    {
        return item;
    }

    public void UseItem()
    {
        Debug.Log("Using " + item.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Pointer Click");
    }
}
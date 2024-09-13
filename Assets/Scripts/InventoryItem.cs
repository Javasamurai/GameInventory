using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    private Item item;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TMPro.TextMeshProUGUI nameText;

    public void Init(Item item){
        this.item = item;
        icon.sprite = item.icon;
        nameText.text = item.name;
    }
    public void SetItem(Item item){
        this.item = item;
    }
    public Item GetItem(){
        return item;
    }
    public void UseItem(){
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
}
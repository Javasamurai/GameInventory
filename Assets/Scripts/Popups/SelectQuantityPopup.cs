using UnityEngine;

public class SelectQuantityPopup : MonoBehaviour {
    [SerializeField] private TMPro.TextMeshProUGUI quantityText;
    private int quantity = 1;

    public void IncreaseQuantity() {
        quantity++;
        quantityText.text = quantity.ToString();
    }

    public void DecreaseQuantity() {
        if (quantity > 1) {
            quantity--;
            quantityText.text = quantity.ToString();
        }
    }

    public void Confirm() {
        // Close popup
        // Add item to inventory
    }
}
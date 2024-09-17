using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PopupManager : GenericMonoSingleton<PopupManager>
{
    [SerializeField] private BuySellPopup buySellPopup;

    protected override void Awake()
    {
        base.Awake();
        buySellPopup.gameObject.SetActive(false);
    }

    public void ShowBuySellPopup(Item item, int quantity, INVENTORY_TYPE inventoryType)
    {
        buySellPopup.Init(item, quantity, inventoryType);
    }
}
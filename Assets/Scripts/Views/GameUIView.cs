using System;
using TMPro;
using UnityEngine;

public class GameUIView : MonoBehaviour {
    [SerializeField]
    TextMeshProUGUI coinsText;

    private void Awake()
    {
        EventService.Instance.OnItemPurchased.AddListener(UpdateUIView);
        EventService.Instance.OnItemSold.AddListener(UpdateUIView);
        UpdateUIView(null);
    }

    private void UpdateUIView(Item item)
    {
        coinsText.text = PlayerWallet.Coins.ToString();
    }
}
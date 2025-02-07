using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject[] itemImages;
    [SerializeField] TMP_Text msgbox;
    private string[] itemDescription;
    private string[] itemIDs;
    private int[] itemPrices;
    private int currItem;
    [SerializeField] TMP_Text currentAD;

    // Start is called before the first frame update
    void Start()
    {
        GetCatalog();
        currItem = 0;
        itemDescription = new string[itemImages.Length];
        itemIDs = new string[itemImages.Length];
        itemPrices = new int[itemImages.Length];

        PFInventoryMgt.Instance.GetVirtualCurrencies();
        currentAD.text = "AD: " + PFInventoryMgt.Instance.coins;
    }

    public void RightChangeButton()
    {
        if (currItem + 1 < itemImages.Length)
        {
            currItem += 1;
            itemImages[currItem].SetActive(true);
            itemImages[currItem - 1].SetActive(false);
            msgbox.text = itemDescription[currItem];
        }
        else
        {
            currItem = 0;
            itemImages[currItem].SetActive(true);
            itemImages[itemImages.Length - 1].SetActive(false);
            msgbox.text = itemDescription[currItem];
        }
    }
    public void LeftChangeButton()
    {
        if (currItem - 1 >= 0)
        {
            currItem -= 1;
            itemImages[currItem].SetActive(true);
            itemImages[currItem + 1].SetActive(false);
            msgbox.text = itemDescription[currItem];
        }
        else
        {
            currItem = itemImages.Length;
            itemImages[currItem].SetActive(true);
            itemImages[0].SetActive(false);
            msgbox.text = itemDescription[currItem];
        }
    }
    public void OnBuyItemBtnPressed()
    {
        var buyreq = new PurchaseItemRequest
        {
            CatalogVersion = "main",
            ItemId = itemIDs[currItem],
            VirtualCurrency = "AD",
            Price = itemPrices[currItem],
        };
        PlayFabClientAPI.PurchaseItem(buyreq,
                                    result => { msgbox.text = itemDescription[currItem] + "\nBought!"; },
                                    error => { msgbox.text = itemDescription[currItem] + "\n" + error.GenerateErrorReport(); });

        PFInventoryMgt.Instance.GetVirtualCurrencies();
    }

    public void GetCatalog()
    {
        var catreq = new GetCatalogItemsRequest
        {
            CatalogVersion = "main"
        };
        PlayFabClientAPI.GetCatalogItems(catreq,
                                        result => {
                                            List<CatalogItem> items = result.Catalog;
                                            Debug.Log("Catalog Items");
                                            int index = 0;
                                            foreach (CatalogItem item in items)
                                            {
                                                if (item.VirtualCurrencyPrices != null && item.VirtualCurrencyPrices.ContainsKey("AD"))
                                                {
                                                    itemDescription[index] = item.DisplayName + "\nPrice:" + item.VirtualCurrencyPrices["AD"] + "\n" + item.Description;
                                                    itemIDs[index] = item.ItemId;
                                                    itemPrices[index] = (int)item.VirtualCurrencyPrices["AD"];
                                                    index++;
                                                }
                                                else
                                                {
                                                    // Handle the case where the item has no price for "AD" currency
                                                    itemDescription[index] = item.DisplayName + "\nPrice: N/A" + "\n" + item.Description;
                                                    index++;

                                                }
                                            }
                                            msgbox.text = itemDescription[0];

                                        }, OnError);

    }
    void OnError(PlayFabError e)
    {
        Debug.Log(e.GenerateErrorReport());
    }
}

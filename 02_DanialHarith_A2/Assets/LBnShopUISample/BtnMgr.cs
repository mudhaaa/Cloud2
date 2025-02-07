using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BtnMgr : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonContainer;

    void Start()
    {
        CreateBuyButton(1, "Sword", 20);
        CreateBuyButton(2, "Shield", 12);
        CreateBuyButton(3, "Armor", 100);
        CreateBuyButton(4, "Bow", 50);
    }
    
    void CreateBuyButton(int itemID,string itemName,int itemPrice)
    {
        GameObject button = Instantiate(buttonPrefab, buttonContainer);
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Buy " + itemName +"["+itemPrice+"]";
        button.GetComponent<Button>().onClick.AddListener(() => OnButtonBuyItem(itemID, itemName, itemPrice));
    }

    void OnButtonBuyItem(int itemID, string itemName, int price)
    {
        Debug.Log("Buying item=>ID: " + itemID+", Name: "+itemName+", Price: "+price);
    }
}

using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private int[] quantity = { 0,0,0};
    [SerializeField] TMP_Text[] texts = new TMP_Text[3];
    [SerializeField] GameObject button;


    // Start is called before the first frame update
    void Start()
    {
        GetPlayerInventory();
        button.SetActive(true);
    }

    public void OnButtonUpdateInventory()
    {
        GetPlayerInventory();

        button.SetActive(false);
    }

    public void GetPlayerInventory()
    {
        var UserInv = new GetUserInventoryRequest();
        PlayFabClientAPI.GetUserInventory(UserInv,
                                        result => {
                                            List<ItemInstance> ii = result.Inventory;
                                            Debug.Log("Player Inventory");
                                            foreach (ItemInstance i in ii)
                                            {
                                                if(i.DisplayName == "Super Laser Cannon")
                                                    quantity[0]++;
                                                if (i.DisplayName == "Jet Thrusters")
                                                    quantity[1]++;
                                                if (i.DisplayName == "Alien Crown")
                                                    quantity[2]++;

                                            }
                                        }, OnError);

        texts[0].text = "Super Laser Cannon\nQuantity Owned: " + quantity[0];
        texts[1].text = "Jet Thrusters\nQuantity Owned: " + quantity[1];
        texts[2].text = "Alien Crown\nQuantity Owned: " + quantity[2];
    }

    void OnError(PlayFabError e)
    {
        Debug.Log(e.GenerateErrorReport());
    }
}

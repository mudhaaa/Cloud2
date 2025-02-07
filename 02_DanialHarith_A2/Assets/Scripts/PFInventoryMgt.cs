using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFInventoryMgt : MonoBehaviour
{
    public static PFInventoryMgt Instance;
    public int coins;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps it alive between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GetVirtualCurrencies()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(),
        r => {
            coins = r.VirtualCurrency["AD"];
            Debug.Log("Debris:" + coins);
        }, OnError);
    }

    public void AddVirtualCurrencies(int amount)
    {
        Debug.Log("Adding AD");
        var req = new AddUserVirtualCurrencyRequest
        {
            VirtualCurrency = "AD",
            Amount = amount
        };

        PlayFabClientAPI.AddUserVirtualCurrency(req,
            result => { Debug.Log("New balance: " + Instance.coins); },
            error => { Debug.Log(error.GenerateErrorReport()); });
    }

    void OnError(PlayFabError e)
    {
        Debug.Log(e.GenerateErrorReport());
    }
}

using PlayFab;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUpdate : MonoBehaviour
{
    [SerializeField] TMP_Text msgbox;

    private void Start()
    {
        PFUserMgt.Instance.GetXP(PlayFabSettings.staticPlayer.PlayFabId);
        PFInventoryMgt.Instance.GetVirtualCurrencies();

        if (!PFUserMgt.Instance.isGuest)
        {
            msgbox.text = PFUserMgt.Instance.username + "\n" + 
                            PFUserMgt.Instance.ClientGetTitleData() + "\n" +
                            PFUserMgt.Instance.xp + "\n" +
                            "AD: " + PFInventoryMgt.Instance.coins; 
        }
        else
        {
            msgbox.text = "Guest Account" + "\n" + 
                            PFUserMgt.Instance.ClientGetTitleData();
        }
    }

    public void OnLogOut()
    {
        PlayFabClientAPI.ForgetAllCredentials();
        SceneManager.LoadScene("RegLogInScene");
        PFUserMgt.Instance.isGuest = false;
        PFUserMgt.Instance.username = "";
    }
}

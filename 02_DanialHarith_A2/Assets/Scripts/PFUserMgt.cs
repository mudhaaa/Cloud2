using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
//using System;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
//using UnityEngine.UI;

public class PFUserMgt : MonoBehaviour
{
    [SerializeField] TMP_Text msgbox;
    [SerializeField] TMP_InputField usernameIF, emailIF, passwordIF;

    public string username;
    public int xpValue;
    public string xp;
    public bool isGuest = false;
    public static PFUserMgt Instance;

    string MOTD = "Message of The Day:";

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

    public void OnButtonRegUser()
    {
        var regReq = new RegisterPlayFabUserRequest
        {
            Email = emailIF.text,
            Username = usernameIF.text,
            Password = passwordIF.text,
            DisplayName = usernameIF.text,
        };
        PlayFabClientAPI.RegisterPlayFabUser(regReq, OnRegSucc, OnError);
    }
    public void OnButtonEmailLogIn()
    {
        var loginReq = new LoginWithEmailAddressRequest
        {
            Email = emailIF.text,
            Password = passwordIF.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(loginReq, OnLoginSucc, OnError);
    }
    public void OnButtonUsernameLogIn()
    {
        var loginRequest = new LoginWithPlayFabRequest
        {
            Username = usernameIF.text,
            Password = passwordIF.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithPlayFab(loginRequest, OnLoginSucc, OnError);
    }
    public void OnButtonGuestLogIn()
    {
        var loginRequest = new LoginWithPlayFabRequest
        {
            Username = "GuestID",
            Password = "GuestPassword"
        };
        PlayFabClientAPI.LoginWithPlayFab(loginRequest, OnLoginSucc, OnError);
        isGuest = true;
    }

    private void OnLoginSucc(LoginResult result)
    {
        username = usernameIF.text;
        msgbox.text = "Log In Success!" + result.PlayFabId;
        SceneManager.LoadScene("Menu");
    }
    private void OnRegSucc(RegisterPlayFabUserResult result)
    {
        username = usernameIF.text;
        msgbox.text = "Register Success!" + result.PlayFabId;
        SceneManager.LoadScene("Menu");
    }

    public void OnResetPassword()
    {
        var ResetPassReq = new SendAccountRecoveryEmailRequest
        {
            Email = emailIF.text,
            TitleId = PlayFabSettings.TitleId
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(ResetPassReq, onResetPassSucc, OnError);
    }
    void onResetPassSucc(SendAccountRecoveryEmailResult r)
    {
        msgbox.text = "Recovery email sent! Please check your email!";
    }

    void UpdateMsg(string msg)
    {
        msgbox.text = msg;
    }
    public string ClientGetTitleData()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(),
            result => {
                if (result.Data == null || !result.Data.ContainsKey("MOTD"))
                {
                    MOTD = "No Message of The Day";
                }
                else
                {
                    MOTD = result.Data["MOTD"];
                }
            },
            error =>
            {
                UpdateMsg("Got error getting titleData:");
                UpdateMsg(error.GenerateErrorReport());
            }
        );
        return MOTD;
    }


    private void OnError(PlayFabError error)
    {
        msgbox.text = "FAILURE!" + error.GenerateErrorReport();
    }


    public void GetXP(string myPlayFabId)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(){
            PlayFabId = myPlayFabId
            // Keys = null
        }, (result) => {
            Debug.Log("Got user data:");
            if (result.Data == null || !result.Data.ContainsKey("XP")) { Debug.Log("No XP"); }
            else 
            {
                Debug.Log("XP: " + result.Data["XP"].Value);
                int loadedXP = int.Parse(result.Data["XP"].Value); // Convert back to int
                xpValue = loadedXP;
                xp = "Total Score:" + loadedXP;
            }
        }, (error) => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());

        });       
    }
    public void SetXP(int xpToGain)
    {
        int newXP = xpValue + xpToGain;
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest(){
            Data = new Dictionary<string, string>() 
            {
                { "XP", newXP.ToString() }
            },
        },result => Debug.Log("Successfully updated user data"),
        error => {
            Debug.Log("Got error setting user data XP");
            Debug.Log(error.GenerateErrorReport());
        });
    }


}

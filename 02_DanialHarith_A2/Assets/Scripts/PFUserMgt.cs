using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class PFUserMgt : MonoBehaviour
{
    [SerializeField] TMP_Text msgbox;
    [SerializeField] TMP_InputField usernameIF, emailIF, passwordIF;

    public void OnButtonRegUser()
    {
        var regReq = new RegisterPlayFabUserRequest
        {
            Email = emailIF.text,
            Username = usernameIF.text,
            Password = passwordIF.text
        };
        PlayFabClientAPI.RegisterPlayFabUser(regReq, OnRegSucc, OnError);
    }

    public void OnButtonLogIn()
    {
        var loginReq = new LoginWithEmailAddressRequest
        {
            Email = emailIF.text,
            Password = passwordIF.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(loginReq, OnLoginSucc, OnError);
    }

    private void OnLoginSucc(LoginResult result)
    {
        msgbox.text = "Log In Success!" + result.PlayFabId;
        SceneManager.LoadScene("GameScene");
    }

    private void OnRegSucc(RegisterPlayFabUserResult result)
    {
        msgbox.text = "Register Success!" + result.PlayFabId;
        SceneManager.LoadScene("GameScene");
    }

    private void OnError(PlayFabError error)
    {
        msgbox.text = "FAILURE!" + error.GenerateErrorReport();
    }

}

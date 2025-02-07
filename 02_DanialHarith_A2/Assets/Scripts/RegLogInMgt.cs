using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegLogInMgt : MonoBehaviour
{
    [SerializeField] GameObject[] buttons;
    int currBtn;
    private void Start()
    {
        for (int i = 1; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
    }

    public void Register()
    {
        PFUserMgt.Instance.OnButtonRegUser();
    }

    public void UsernameLogIn()
    {
        PFUserMgt.Instance.OnButtonUsernameLogIn();
    }

    public void EmailLogIn() 
    {
        PFUserMgt.Instance.OnButtonEmailLogIn();
    }

    public void ResetPassword()
    {
        PFUserMgt.Instance.OnResetPassword();
    }

    public void GuestLogIn()
    {
        PFUserMgt.Instance.OnButtonGuestLogIn();
    }

    public void RightChangeButton()
    {
        if (currBtn + 1 < buttons.Length)
        {
            currBtn += 1;
            buttons[currBtn].SetActive(true);
            buttons[currBtn - 1].SetActive(false);
        }
        else
        {
            currBtn = 0;
            buttons[currBtn].SetActive(true);
            buttons[buttons.Length - 1].SetActive(false);
        }
    }
    public void LeftChangeButton()
    {
        if (currBtn - 1 >= 0)
        {
            currBtn -= 1;
            buttons[currBtn].SetActive(true);
            buttons[currBtn + 1].SetActive(false);
        }
        else
        {
            currBtn = buttons.Length;
            buttons[currBtn].SetActive(true);
            buttons[0].SetActive(false);
        }
    }
}

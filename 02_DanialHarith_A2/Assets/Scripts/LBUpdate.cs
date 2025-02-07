using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LBUpdate : MonoBehaviour
{
    [SerializeField] TMP_Text msgbox;

    private void Start()
    {
        PFLeaderboardMgt.Instance.OnGetLeaderboard();
        msgbox.text = PFLeaderboardMgt.Instance.LeaderboardStr;
    }
    public void GetLeaderboardButton()
    {
        PFLeaderboardMgt.Instance.OnGetLeaderboard();
        msgbox.text = PFLeaderboardMgt.Instance.LeaderboardStr;
    }
}

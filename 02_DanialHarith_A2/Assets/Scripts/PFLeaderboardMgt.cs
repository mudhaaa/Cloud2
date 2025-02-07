using PlayFab.ClientModels;
using PlayFab;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using PlayFab.MultiplayerModels;

public class PFLeaderboardMgt : MonoBehaviour
{
    [SerializeField] TMP_Text msgbox;

    public static PFLeaderboardMgt Instance;
    private int currentScore;
    public string LeaderboardStr;

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

    public void SetScore(int i)
    {
        currentScore = i;
    }

    void UpdateMsg(string msg)
    {
        msgbox.text = msg;
    }
    private void OnError(PlayFabError error)
    {
        Debug.Log("FAILURE!" + error.GenerateErrorReport());
    }


    public void OnGetLeaderboard() {
        var lbreq = new GetLeaderboardRequest
        {
            StatisticName = "highscore", //playfab leaderboard statistic names
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(lbreq, OnLeaderboardGet, OnError);  
    }
    void OnLeaderboardGet(GetLeaderboardResult r)
    {
        LeaderboardStr = "Leaderboard\n";
        foreach (var item in r.Leaderboard)
        { 
            int position = item.Position + 1;
            string onerow = "Position: " + position + "/Name: " + item.DisplayName + "/Score: " + item.StatValue + "\n";
            LeaderboardStr += onerow; //combine all display into one string 1.
        }
    }

    public void OnButtonSendLeaderboard()
    {
        var req = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>{ //playfab leaderboard statistic name
                new StatisticUpdate
                {
                    StatisticName="highscore",
                    Value=PFUserMgt.Instance.xpValue
                }
            }
        };

        Debug.Log("Submitting score:" + PFUserMgt.Instance.xpValue);
        PlayFabClientAPI.UpdatePlayerStatistics(req, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult r)
    {
        Debug.Log("Successful leaderboard sent:" + r.ToString());
    }
}

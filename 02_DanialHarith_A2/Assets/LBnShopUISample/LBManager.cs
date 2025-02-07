using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LBManager : MonoBehaviour
{
    public GameObject scoreRowPrefab;
    public Transform scoreContainer;

    public void AddScore(int rank,string playerName, int score)
    {       
        GameObject oneRow = Instantiate(scoreRowPrefab, scoreContainer);
        TextMeshProUGUI a = oneRow.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        Debug.Log(a);
        oneRow.transform.Find("Rank").GetComponent<TextMeshProUGUI>().text = rank.ToString();
        oneRow.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = playerName;
        oneRow.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

    private void ClearLeaderBoard()
    {
        foreach (Transform child in scoreContainer)
        {
            Destroy(child.gameObject);
        }
    }
    void Start()
    {
        ClearLeaderBoard();
        //scoreContainer.Clear();
        AddScore(1,"Tom", 100);
        AddScore(2,"Dick", 123);
        AddScore(3,"John", 445);
        AddScore(4,"Harry", 456);
    }
}



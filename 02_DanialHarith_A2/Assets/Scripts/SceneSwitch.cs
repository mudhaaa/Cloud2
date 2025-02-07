using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ToLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void ToInventory()
    {
        SceneManager.LoadScene("Inventory");
    }    
    
    public void ToShop()
    {
        SceneManager.LoadScene("Shop");
    }
}

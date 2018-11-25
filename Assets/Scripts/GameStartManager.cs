using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartManager : MonoBehaviour
{
    public void OnSelectButtonClick(string typeName)
    {
        PlayerType type = (PlayerType)System.Enum.Parse(typeof(PlayerType), typeName);
        InitialPlayerStats.Type = type;
        SceneManager.LoadScene("Play Field");
    }
}

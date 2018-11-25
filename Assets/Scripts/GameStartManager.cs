using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartManager : MonoBehaviour
{
    void Start()
    {
        InitialPlayerStats.Type = PlayerType.House;
    }

    public void OnClick()
    {
        SceneManager.LoadScene("Play Field");
    }
}

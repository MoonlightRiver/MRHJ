using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartManager : MonoBehaviour
{
    public Image background;
    public Sprite[] startAnimation;

    void Start()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        foreach (Sprite sprite in startAnimation)
        {
            background.sprite = sprite;

            yield return new WaitForSeconds(1);
        }
    }

    public void OnSelectButtonClick(string typeName)
    {
        PlayerType type = (PlayerType)System.Enum.Parse(typeof(PlayerType), typeName);
        InitialPlayerStats.Type = type;
        SceneManager.LoadScene("Play Field");
    }
}

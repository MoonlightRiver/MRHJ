using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject statusPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            statusPanel.SetActive(!statusPanel.activeSelf);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleWindow : MonoBehaviour
{
    public Button ExitButton;

    void Start()
    {
        ExitButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("Exit Window");
        var computer = GameObject.Find("Computer").GetComponent<Computer>();
        computer.Toggle();
    }
}

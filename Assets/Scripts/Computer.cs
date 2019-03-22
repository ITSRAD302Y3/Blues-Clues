using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Computer : MonoBehaviour {

    public GameObject ComputerUI;

    public bool IsOpened = false;

    private void Start()
    {
        enabled = false;
    }

    public virtual void Open()
    {
        IsOpened = true;

        var canvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();

        var control = Instantiate(ComputerUI, canvas.transform);

        FirstPersonController.IsPaused = true;
        Cursor.visible = true;
    }

    public virtual void Close()
    {
        Cursor.visible = false;
        FirstPersonController.IsPaused = false;

        IsOpened = false;
    }

    public virtual void Toggle()
    {
        if (IsOpened)
            Close();
        else
            Open();
    }
}

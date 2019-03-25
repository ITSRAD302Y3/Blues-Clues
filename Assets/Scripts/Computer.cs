using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Computer : MonoBehaviour
{
    public GameObject ComputerScreen;
    public GameObject VirtualCamera;
    public GameObject Flashlight;
    public GameObject MainCanvas;

    public bool IsOpened = false;

    private void Start()
    {
        MainCanvas = GameObject.FindWithTag("MainCanvas");
    }

    public virtual void Open()
    {
        IsOpened = true;

        VirtualCamera.SetActive(true);
        ComputerScreen.SetActive(true);

        Flashlight.SetActive(false);
        MainCanvas.SetActive(false);

        FirstPersonController.IsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public virtual void Close()
    {                
        FirstPersonController.IsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        Flashlight.SetActive(true);
        ComputerScreen.SetActive(false);
        VirtualCamera.SetActive(false);
        MainCanvas.SetActive(true);

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

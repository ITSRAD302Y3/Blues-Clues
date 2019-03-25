using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    public GameObject VCam;
    public GameObject MainCanvas;
    public GameObject Computer;

    private void Start()
    {        
        if (Player == null) Player = GameObject.FindWithTag("Player");
        if (MainCanvas == null) MainCanvas = GameObject.FindWithTag("MainCanvas");
        if (Computer == null) Computer = GameObject.Find("PC");
    }

    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GetComponent<CinemachineVirtualCamera>().enabled = false;

        var panel = MainCanvas.transform.Find("Panel").gameObject;

        switch (panel.transform.Find("Dropdown").GetComponent<Dropdown>().value)
        {
            case 0:
                // Answerer
                Player.transform.Find("VCam1").gameObject.SetActive(true);
                break;
            case 1:
                // Locator
                Player.transform.position = new Vector3(-7.22f, 1.04f, 13.38f);
                Player.transform.Find("VCam1").gameObject.SetActive(true);
                Computer.transform.position = new Vector3(-10.783f, 0.059f, 11.589f);
                break;
            default:
                Player.transform.Find("VCam1").gameObject.SetActive(true);
                break;
        }

        panel.SetActive(false);
    }
}

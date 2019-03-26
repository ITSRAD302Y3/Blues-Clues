using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GameController : MonoBehaviour
{
    public static string PlayerType;

    public GameObject Player;
    public GameObject VCam;
    public GameObject MainCanvas;
    public GameObject Computer;
    public GameObject ApiController;
    public GameObject Door;

    private void Start()
    {
        if (Player == null) Player = GameObject.FindWithTag("Player");
        if (MainCanvas == null) MainCanvas = GameObject.FindWithTag("MainCanvas");
        if (Computer == null) Computer = GameObject.Find("PC");
        if (ApiController == null) ApiController = GameObject.Find("APIController");
    }

    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GetComponent<CinemachineVirtualCamera>().enabled = false;

        var panel = MainCanvas.transform.Find("Panel").gameObject;
        var dropDown = panel.transform.Find("Dropdown").GetComponent<Dropdown>();
        PlayerType = dropDown.options[dropDown.value].text;

        switch (dropDown.value)
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
                Computer.transform.Find("Screen/Canvas/ChoosePanel").gameObject.SetActive(false);
                Computer.transform.Find("Screen/Canvas/ChatPanel").transform.localPosition = Vector3.zero;
                break;
            default:
                Player.transform.Find("VCam1").gameObject.SetActive(true);
                break;
        }

        panel.SetActive(false);
    }

    public void EndGame()
    {
        Player.GetComponent<PlayerSelectionController>().enabled = false;

        var text = MainCanvas.transform.Find("SelectedText").gameObject;
        text.SetActive(true);

        var toggle = Computer.transform.Find("Screen/Canvas/ChoosePanel/ImagePanel").GetComponent<ToggleGroup>().ActiveToggles().FirstOrDefault();

        var controller = ApiController.GetComponent<Imgur>();

        if (int.Parse(toggle.name) == controller.ChosenComputerImage)
        {
            text.GetComponent<Text>().text = "Game Complete - You Win";
            
            Door.GetComponent<Animator>().SetTrigger("open");
            Door.GetComponent<AudioSource>().Play();
        }
        else
        {
            text.GetComponent<Text>().text = "Game Over - You Lose";           
        }
    }
}

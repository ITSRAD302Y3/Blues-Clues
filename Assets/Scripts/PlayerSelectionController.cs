using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionController : MonoBehaviour {

    public string SelectedObjectTag;
    public GameObject SelectedObject;
    public float MaxInteractionDistance = 500;
    private RaycastHit _hitResult;

    public Text txtSelected;

    void Update()
    {
        if (Physics.Raycast(
            Camera.main.transform.position,
            Camera.main.transform.forward,
            out _hitResult,
            MaxInteractionDistance))
        {
            if (_hitResult.collider.gameObject.CompareTag("Computer"))
            {
                SelectedObject = _hitResult.collider.gameObject;
                SelectedObjectTag = SelectedObject.tag;
                ShowText();
            }
            else
            {
                SelectedObject = null;
                SelectedObjectTag = "";
                HideText();
            }

            if (!string.IsNullOrEmpty(SelectedObjectTag))
            {
                txtSelected.text = "Press E to use " + SelectedObjectTag;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
            if (SelectedObject != null)
                HandleSelection();
    }

    private void HandleSelection()
    {
        if (SelectedObject != null)
        {
            switch (SelectedObjectTag)
            {
                case "Computer":
                    SelectedObject.GetComponent<Computer>().Toggle();
                    break;
            }
        }
    }

    private void ShowText()
    {
        txtSelected.gameObject.SetActive(true);
    }

    private void HideText()
    {
        txtSelected.gameObject.SetActive(false);
    }
}

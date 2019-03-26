using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFade : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Animator>().Play("TextFadeIn");
    }
}

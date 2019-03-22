using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerTrigger : MonoBehaviour
{
    public Texture m_MainTexture, m_AwakeTexture;
    private Renderer m_Renderer;

    private void Start()
    {
        m_Renderer = GameObject.Find("Screen").GetComponent<Renderer>();
        m_Renderer.material.mainTexture = m_MainTexture;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_Renderer.material.mainTexture = m_AwakeTexture;
            GetComponent<AudioSource>().Play();
            GameObject.Find("Screen").GetComponent<Computer>().enabled = true;
        }
    }
}

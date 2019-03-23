using System.Collections;
using System.Collections.Generic;
using AuraAPI;
using UnityEngine;

public class FlashlightToggle : MonoBehaviour
{
    private AuraLight _auraLight;
    private Light _light;

    void Start()
    {
        _light = GetComponentInChildren<Light>();
        _auraLight = GetComponentInChildren<AuraLight>();
    }

	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.F))
	    {
	        _light.enabled = !_light.enabled;
            _auraLight.enabled = !_auraLight.enabled;
        }	        
	}
}
